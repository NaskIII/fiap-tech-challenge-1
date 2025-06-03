using Application.Dtos.KitchenDtos.Response;

namespace Application.Interfaces
{
    public interface IKitchenQueueListQueue : IUseCase<object?, List<KitchenQueueResponse>>
    {
    }
}
