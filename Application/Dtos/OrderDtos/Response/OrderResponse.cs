using Application.Dtos.Client.Response;
using Application.Dtos.OrderItemDtos.Response;

namespace Application.Dtos.OrderDtos.Response
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid? ClientId { get; set; }
        public ClientResponse Client {  get; set; } = default!;
        public string OrderStatus { get; set; } = default!;
        public List<OrderItemResponse> OrderItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
}
