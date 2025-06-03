using Application.Dtos.OrderDtos.Request;
using Application.Dtos.OrderDtos.Response;
using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers.V1
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/v1/order")]
    public class OrderController : ControllerBase
    {

        private readonly ICreateOrderUseCase _createOrderUseCase;
        private readonly IFilterOrderUseCase _filterOrderUseCase;
        private readonly IGetOrderUseCase _getOrderUseCase;

        private readonly IConfiguration _configuration;

        public OrderController(
            ICreateOrderUseCase createOrderUseCase,
            IFilterOrderUseCase filterOrderUseCase,
            IGetOrderUseCase getOrderUseCase,
            IConfiguration configuration)
        {
            _createOrderUseCase = createOrderUseCase;
            _filterOrderUseCase = filterOrderUseCase;
            _getOrderUseCase = getOrderUseCase;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] OrderRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Invalid order request.");
                }

                var orderId = await _createOrderUseCase.ExecuteAsync(request);

                string? systemUrl = _configuration.GetSection("System").GetValue<string>("BaseURL");
                if (string.IsNullOrEmpty(systemUrl))
                    throw new ArgumentNullException(nameof(systemUrl));

                var response = Created();

                response.Location = $"{systemUrl}/order/{orderId}";

                return response;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao criar o pedido: " + ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterOrderRequest request)
        {
            try
            {
                var orders = await _filterOrderUseCase.ExecuteAsync(request);
                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao filtrar os pedidos: " + ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResponse>> GetOrderAsync(Guid id)
        {
            try
            {
                OrderResponse order = await _getOrderUseCase.ExecuteAsync(id);
                return Ok(order);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao obter o pedido: " + ex.Message);
            }
        }
    }
}
