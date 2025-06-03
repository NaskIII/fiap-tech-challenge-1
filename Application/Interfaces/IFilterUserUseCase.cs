using Application.Dtos.UserDtos.Request;
using Application.Dtos.UserDtos.Response;

namespace Application.Interfaces
{
    public interface IFilterUserUseCase : IUseCase<FilterUserRequest, List<UserSummaryResponse>>
    {
    }
}
