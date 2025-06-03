using Application.Dtos.CheckoutDtos.Request;

namespace Application.Interfaces
{
    public interface ICheckoutUseCase : IUseCase<CheckoutRequest, Guid>
    {
    }
}
