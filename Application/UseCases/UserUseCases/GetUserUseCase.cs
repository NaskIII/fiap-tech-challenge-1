using Application.Dtos.UserDtos.Response;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.UserUseCases
{
    public class GetUserUseCase : IGetUserUseCase
    {

        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public GetUserUseCase(IUserRepository userRepository, ILogger<GetUserUseCase> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserSummaryResponse> ExecuteAsync(Guid request)
        {
            User? user = await _userRepository.GetByIdAsync(request);

            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", request);
                throw new NotFoundException($"O Usuário não foi encontrado.");
            }

            UserSummaryResponse response = _mapper.Map<UserSummaryResponse>(user);

            return response;
        }
    }
}
