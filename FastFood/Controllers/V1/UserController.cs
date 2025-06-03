using Application.Dtos.UserDtos.Request;
using Application.Dtos.UserDtos.Response;
using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers.V1
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {

        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly IFilterUserUseCase _filterUserUseCase;
        private readonly IGetUserUseCase _getUserUseCase;

        private readonly IConfiguration _configuration;

        public UserController(
            IRegisterUserUseCase registerUserUseCase,
            IFilterUserUseCase filterUserUseCase,
            IConfiguration configuration,
            IGetUserUseCase getUserUseCase
            )
        {
            _registerUserUseCase = registerUserUseCase;
            _filterUserUseCase = filterUserUseCase;
            _configuration = configuration;
            _getUserUseCase = getUserUseCase;
        }

        [HttpPost]
        public async Task<ActionResult<UserSummaryResponse>> Register([FromBody] RegisterUserRequest request)
        {
            try
            {
                UserSummaryResponse userSummary = await _registerUserUseCase.ExecuteAsync(request);

                string? systemUrl = _configuration.GetSection("System").GetValue<string>("BaseURL");
                if (string.IsNullOrEmpty(systemUrl))
                    throw new ArgumentNullException(nameof(systemUrl));

                var response = Created();

                response.Location = $"{systemUrl}/user/{userSummary.UserId}";

                return response;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserSummaryResponse>> GetById(Guid id)
        {
            try
            {

                UserSummaryResponse user = await _getUserUseCase.ExecuteAsync(id);
                return Ok(user);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<UserSummaryResponse>>> Filter([FromQuery] FilterUserRequest request)
        {
            List<UserSummaryResponse> users = await _filterUserUseCase.ExecuteAsync(request);
            return Ok(users);
        }
    }
}
