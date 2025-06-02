using Application.Dtos.Client.Request;
using Application.Dtos.Client.Response;

namespace Application.Interfaces
{
    public interface IFilterClientsUseCase : IUseCase<FilterClientsRequest, List<ClientSummaryResponse>>
    {
    }
}
