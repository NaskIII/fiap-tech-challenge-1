using Application.Dtos.Client.Request;

namespace Application.Interfaces
{
    public interface IRegisterClientUseCase : IUseCase<RegisterClientRequest, Guid>
    {
    }
}
