using Application.Dtos.Client.Request;
using Application.Dtos.Client.Response;
using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers.V1
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/client")]
    public class ClientController : ControllerBase
    {

        private readonly IRegisterClientUseCase _registerClientUseCase;
        private readonly IFilterClientsUseCase _filterClientsUseCase;
        private readonly IRetrieveClientUseCase _retrieveClientUseCase;

        private readonly IConfiguration _configuration;

        public ClientController(IRegisterClientUseCase registerClientUseCase, IFilterClientsUseCase filterClientsUseCase,
            IRetrieveClientUseCase retrieveClientUseCase, IConfiguration configuration)
        {
            _registerClientUseCase = registerClientUseCase;
            _filterClientsUseCase = filterClientsUseCase;
            _retrieveClientUseCase = retrieveClientUseCase;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterClientAsync([FromBody] RegisterClientRequest request)
        {
            try
            {
                Guid clientId = await _registerClientUseCase.ExecuteAsync(request);

                string? systemUrl = _configuration.GetSection("System").GetValue<string>("BaseURL");
                if (string.IsNullOrEmpty(systemUrl))
                    throw new ArgumentNullException(nameof(systemUrl));

                var response = Created();

                response.Location = $"{systemUrl}/client/{clientId}";

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

        [HttpGet("filter")]
        public async Task<ActionResult<ClientSummaryResponse>> FilterClientsAsync([FromQuery] FilterClientsRequest request)
        {
            if (request == null)
                return BadRequest("Dados de filtro não podem ser nulos.");
            try
            {
                List<ClientSummaryResponse> clients = await _filterClientsUseCase.ExecuteAsync(request);
                return Ok(clients);
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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClientResponse>> RetrieveClientAsync(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("ID do cliente não pode ser vazio.");
            try
            {
                ClientResponse client = await _retrieveClientUseCase.ExecuteAsync(id);
                return Ok(client);
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
