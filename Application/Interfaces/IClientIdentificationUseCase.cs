using Application.Dtos.ClientDtos.Request;
using Application.Dtos.ClientDtos.Response;

namespace Application.Interfaces
{
    public interface IClientIdentificationUseCase : IUseCase<ClientIdentificationRequest, AuthenticatedClientResponse>
    {
    }
}
