namespace Domain.Entities
{
    public class OrderItem
    {

        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
