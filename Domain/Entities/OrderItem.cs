namespace Domain.Entities
{
    public class OrderItem
    {
        public Guid OrderItemId { get; private set; }
        public Guid OrderId { get; private set; }
        public virtual Order Order { get; private set; } = null!;
        public Guid ProductId { get; private set; }
        public virtual Product Product { get; private set; } = null!;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public decimal Total => Quantity * UnitPrice;

        private OrderItem() { }

        public OrderItem(Guid productId, Guid orderId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
            if (unitPrice <= 0) throw new ArgumentException("Unit price must be greater than zero.");

            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public OrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
            if (unitPrice <= 0) throw new ArgumentException("Unit price must be greater than zero.");

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0) throw new ArgumentException("Increase amount must be greater than zero.");
            Quantity += amount;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
            Quantity = newQuantity;
        }

        public void UpdateUnitPrice(decimal newPrice)
        {
            if (newPrice <= 0) throw new ArgumentException("Unit price must be greater than zero.");
            UnitPrice = newPrice;
        }
    }
}
