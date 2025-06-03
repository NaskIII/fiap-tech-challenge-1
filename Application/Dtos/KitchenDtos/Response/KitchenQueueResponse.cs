using Application.Dtos.OrderDtos.Response;

namespace Application.Dtos.KitchenDtos.Response
{
    public class KitchenQueueResponse
    {
        public Guid KitchenQueueId { get; set; }
        public OrderResponse Order { get; set; } = default!;
        public DateTime EnqueuedAt { get; set; }
        public TimeSpan ElapsedTime => DateTime.UtcNow - EnqueuedAt;
    }
}
