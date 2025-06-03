using Application.Dtos.UserDtos.Request;
using Application.Dtos.UserDtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.RepositoryInterfaces;

namespace Application.UseCases.UserUseCases
{
    internal class FilterUserUseCase : IFilterUserUseCase
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FilterUserUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserSummaryResponse>> ExecuteAsync(FilterUserRequest request)
        {
            var users = await _userRepository.FilterUserAsync(request.UserName, request.Email);

            return _mapper.Map<List<UserSummaryResponse>>(users);
        }
    }
}
