

using Application.Dtos.ProductCategory.Request;
using Application.Dtos.ProductCategory.Response;
using Application.Dtos.ProductDtos.Request;
using Application.Dtos.ProductDtos.Response;
using Application.Exceptions;
using Application.Interfaces;
using Application.UseCases.ProductCategoryUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers.V1
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : ControllerBase
    {
        private readonly IFilterProductUseCase _filterProductUseCase;
        private readonly IGetProductUseCase _getProductUseCase;
        private readonly IUpdateProductUseCase _updateProductUseCase;
        private readonly IRegisterProductUseCase _registerProductUseCase;

        private readonly IConfiguration _configuration;

        public ProductController(
            IFilterProductUseCase filterProductUseCase,
            IGetProductUseCase getProductUseCase,
            IUpdateProductUseCase updateProductUseCase,
            IRegisterProductUseCase registerProductUseCase,
            IConfiguration configuration)
        {
            _filterProductUseCase = filterProductUseCase;
            _getProductUseCase = getProductUseCase;
            _updateProductUseCase = updateProductUseCase;
            _registerProductUseCase = registerProductUseCase;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateProductRequest request)
        {
            try
            {
                Guid id = await _registerProductUseCase.ExecuteAsync(request);

                string? systemUrl = _configuration.GetSection("System").GetValue<string>("BaseURL");
                if (string.IsNullOrEmpty(systemUrl))
                    throw new ArgumentNullException(nameof(systemUrl));

                var response = Created();

                response.Location = $"{systemUrl}/product/{id}";

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
        public async Task<ActionResult<List<ProductSummaryResponse>>> FilterAsync([FromQuery] ProductFilterRequest request)
        {
            try
            {
                List<ProductSummaryResponse> productCategories = await _filterProductUseCase.ExecuteAsync(request);
                return Ok(productCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductSummaryResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                ProductSummaryResponse productCategory = await _getProductUseCase.ExecuteAsync(id);
                return Ok(productCategory);
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

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreateProductRequest request)
        {
            try
            {
                if (id != request.ProductCategoryId)
                {
                    return BadRequest("O ID da categoria de produto não corresponde ao ID fornecido na solicitação.");
                }
                await _updateProductUseCase.ExecuteAsync(request);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, $"Erro ao atualizar a categoria de produto: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
