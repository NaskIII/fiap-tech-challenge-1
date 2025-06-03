using Application.Dtos.UserDtos.Request;
using Application.Dtos.UserDtos.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class UserMappers : Profile
    {

        public UserMappers()
        {
            CreateMap<RegisterUserRequest, User>();

            CreateMap<User, UserSummaryResponse>();
            CreateMap<User, AuthenticatedUserResponse>();
        }
    }
}
