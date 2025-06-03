using Application.Dtos.Client.Response;

namespace Application.Interfaces
{
    public interface IRetrieveClientUseCase : IUseCase<Guid, ClientResponse>
    {
    }
}
