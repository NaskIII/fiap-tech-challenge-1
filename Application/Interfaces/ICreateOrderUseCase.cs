using Application.Dtos.OrderDtos.Request;

namespace Application.Interfaces
{
    public interface ICreateOrderUseCase : IUseCase<OrderRequest, Guid>
    {
    }
}
