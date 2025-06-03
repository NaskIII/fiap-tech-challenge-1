using Application.Dtos.UserDtos.Request;
using Application.Dtos.UserDtos.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers.V1
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/user-authentication")]
    public class UserAuthenticationController : ControllerBase
    {

        private readonly IAuthenticateUserUseCase _authenticateUserUseCase;
        public UserAuthenticationController(IAuthenticateUserUseCase authenticateUserUseCase)
        {
            _authenticateUserUseCase = authenticateUserUseCase;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticatedUserResponse>> Authenticate([FromBody] AuthenticateUserRequest request)
        {
            try
            {
                AuthenticatedUserResponse response = await _authenticateUserUseCase.ExecuteAsync(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
