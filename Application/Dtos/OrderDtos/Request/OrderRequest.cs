using Application.Dtos.OrderItemDtos.Request;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.OrderDtos.Request
{
    public class OrderRequest
    {
        public Guid? ClientId { get; set; }

        [Required]
        public List<OrderItemRequest> OrderItems { get; set; } = [];
    }
}
