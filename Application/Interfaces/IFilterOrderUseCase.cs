using Application.Dtos.OrderDtos.Request;
using Application.Dtos.OrderDtos.Response;

namespace Application.Interfaces
{
    public interface IFilterOrderUseCase : IUseCase<FilterOrderRequest, List<OrderResponse>>
    {
    }
}
