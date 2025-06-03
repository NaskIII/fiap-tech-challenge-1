namespace Application.Dtos.OrderItemDtos.Response
{
    public class OrderItemResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }
}
