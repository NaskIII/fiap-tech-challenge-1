using Application.Dtos.OrderDtos.Response;

namespace Application.Interfaces
{
    public interface IGetOrderUseCase : IUseCase<Guid, OrderResponse>
    {
    }
}
