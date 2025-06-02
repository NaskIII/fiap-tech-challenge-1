using Application.Dtos.ClientDtos.Request;
using Application.Dtos.ClientDtos.Response;
using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers.V1
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/auth-client")]
    public class ClientAuthenticationController : ControllerBase
    {

        private readonly IClientIdentificationUseCase _clientIdentificationUseCase;

        public ClientAuthenticationController(IClientIdentificationUseCase clientIdentificationUseCase)
        {
            _clientIdentificationUseCase = clientIdentificationUseCase;
        }

        [HttpPost("identify")]
        public async Task<ActionResult<AuthenticatedClientResponse>> IdentifyClientAsync([FromBody] ClientIdentificationRequest request)
        {
            if (request == null)
                return BadRequest("Dados do cliente não podem ser nulos.");
            try
            {
                var response = await _clientIdentificationUseCase.ExecuteAsync(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
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
    }
}
