
using Application.Dtos.UserDtos.Request;
using Application.Dtos.UserDtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.Security;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.UserUseCases
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterUserUseCase> _logger;

        public RegisterUserUseCase(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IMapper mapper,
            ILogger<RegisterUserUseCase> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserSummaryResponse> ExecuteAsync(RegisterUserRequest request)
        {
            try
            {
                var email = new Email(request.Email);
                var passwordHash = _passwordHasher.HashPassword(request.Password);

                var user = new User(request.UserName, email, passwordHash);

                await _userRepository.AddAsync(user);

                return _mapper.Map<UserSummaryResponse>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar usuário.");
                throw;
            }
        }
    }
}
