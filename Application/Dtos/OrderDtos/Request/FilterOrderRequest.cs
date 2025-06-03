using Domain.Enums;

namespace Application.Dtos.OrderDtos.Request
{
    public class FilterOrderRequest
    {
        public DateTime? OrderDate { get; set; }
        public Guid? ClientId { get; set; }
        public OrderStatus? OrderStatus { get; set; }
    }
}
