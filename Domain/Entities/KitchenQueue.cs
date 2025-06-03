using Domain.Enums;

namespace Domain.Entities
{
    public class KitchenQueue
    {
        public Guid KitchenQueueId { get; private set; }
        public Guid OrderId { get; private set; }
        public virtual Order Order { get; private set; } = default!;
        public DateTime EnqueuedAt { get; private set; }

        public KitchenQueue(Order order)
        {
            Order = order;
            EnqueuedAt = DateTime.UtcNow;
        }

        public KitchenQueue() { }

        public void ChangeOrderStatus(OrderStatus status)
        {
            if (Order.OrderStatus == status)
            {
                return;
            }

            Order.ChangeStatus(status);
        }

        public TimeSpan ElapsedTime()
        {
            return DateTime.UtcNow - EnqueuedAt;
        }
    }
}
