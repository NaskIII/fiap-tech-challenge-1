using Application.Dtos.UserDtos.Request;
using Application.Dtos.UserDtos.Response;

namespace Application.Interfaces
{
    public interface IAuthenticateUserUseCase : IUseCase<AuthenticateUserRequest, AuthenticatedUserResponse>
    {
    }
}
