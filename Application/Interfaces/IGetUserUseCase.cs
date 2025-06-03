using Application.Dtos.UserDtos.Response;

namespace Application.Interfaces
{
    public interface IGetUserUseCase : IUseCase<Guid, UserSummaryResponse>
    {
    }
}
