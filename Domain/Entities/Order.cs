using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; private set; }
        public int OrderNumber { get; private set; }
        public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
        public Guid? ClientId { get; private set; }
        public virtual Client? Client { get; private set; }
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;
        public Guid? PaymentId { get; private set; }
        public virtual Payment? Payment { get; private set; }

        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private Order() { }

        public Order(Guid? clientId)
        {
            ClientId = clientId;
        }

        public void AddItem(Product product, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            var existingItem = _orderItems.FirstOrDefault(i => i.ProductId == product.ProductId);
            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                _orderItems.Add(new OrderItem(product.ProductId, OrderId, quantity, product.Price));
            }
        }

        public decimal GetTotal()
        {
            return _orderItems.Sum(i => i.Total);
        }

        public void MarkAsReceived(Guid paymentId)
        {
            if (OrderStatus != OrderStatus.Pending)
                throw new InvalidOperationException("Only pending orders can be marked as received.");

            PaymentId = paymentId;
            OrderStatus = OrderStatus.Received;
        }

        public void Cancel()
        {
            if (OrderStatus == OrderStatus.Completed)
                throw new InvalidOperationException("Completed orders cannot be canceled.");

            OrderStatus = OrderStatus.Canceled;
        }

        public void ChangeStatus(OrderStatus newStatus)
        {
            if (newStatus == OrderStatus.Canceled && OrderStatus == OrderStatus.Completed)
                throw new InvalidOperationException("Completed orders cannot be canceled.");
            OrderStatus = newStatus;
        }
    }

}
