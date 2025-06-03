using Application.Dtos.UserDtos.Request;
using Application.Dtos.UserDtos.Response;
using Application.Interfaces;
using Application.Security;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.UserUseCases
{
    public class AuthenticateUserUseCase : IAuthenticateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        private readonly ILogger<AuthenticateUserUseCase> _logger;
        private readonly IMapper _mapper;

        public AuthenticateUserUseCase(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            ILogger<AuthenticateUserUseCase> logger,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AuthenticatedUserResponse> ExecuteAsync(AuthenticateUserRequest request)
        {
            Email email = new(request.Email);

            _logger.LogInformation("Authenticating user with email: {Email}", email.Value);

            User? user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                _logger.LogWarning("User with email {Email} not found", email.Value);
                throw new ArgumentException("Invalid email or password.");
            }

            bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                _logger.LogWarning("Invalid password for user {UserName}", user.UserName);
                throw new ArgumentException("Invalid email or password.");   
            }

            _logger.LogInformation("User {UserName} authenticated successfully", user.UserName);

            AuthenticatedUserResponse response = _mapper.Map<AuthenticatedUserResponse>(user);

            response.Token = _tokenService.GenerateToken(user.UserId, user.Email.Value, ["Administrador"]);

            return response;
        }
    }
}
