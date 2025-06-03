using Domain.Enums;

namespace Application.Dtos.KitchenDtos.Request
{
    public class ManageKitchenRequest
    {
        public Guid OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
