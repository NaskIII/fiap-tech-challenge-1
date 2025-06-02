using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {

        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Guid? ClientId { get; set; }
        public virtual Client? Client { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Guid? PaymentId { get; set; }
        public virtual Payment? Payment { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
