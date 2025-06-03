using Application.Dtos.ProductCategory.Request;
using Application.Dtos.ProductCategory.Response;
using Application.Dtos.UserDtos.Response;
using Application.Exceptions;
using Application.Interfaces;
using Application.UseCases.UserUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/product-category")]
    public class ProductCategoryController : ControllerBase
    {

        private readonly IRegisterProductCategoryUseCase _registerProductCategoryUseCase;
        private readonly IFilterProductCategoryUseCase _filterProductCategoryUseCase;
        private readonly IGetProductCategoryUseCase _getProductCategoryUseCase;
        private readonly IUpdateProductCategoryUseCase _updateProductCategoryUseCase;

        private readonly IConfiguration _configuration;

        public ProductCategoryController(
            IRegisterProductCategoryUseCase registerProductCategoryUseCase,
            IFilterProductCategoryUseCase filterProductCategoryUseCase,
            IGetProductCategoryUseCase getProductCategoryUseCase,
            IConfiguration configuration)
        {
            _registerProductCategoryUseCase = registerProductCategoryUseCase;
            _filterProductCategoryUseCase = filterProductCategoryUseCase;
            _getProductCategoryUseCase = getProductCategoryUseCase;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateProductCategoryRequest request)
        {
            try
            {
                ProductCategorySummaryResponse summary = await _registerProductCategoryUseCase.ExecuteAsync(request);

                string? systemUrl = _configuration.GetSection("System").GetValue<string>("BaseURL");
                if (string.IsNullOrEmpty(systemUrl))
                    throw new ArgumentNullException(nameof(systemUrl));

                var response = Created();

                response.Location = $"{systemUrl}/product-category/{summary.ProductCategoryId}";

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
        public async Task<ActionResult<List<ProductCategorySummaryResponse>>> FilterAsync([FromQuery] FilterProductCategoryRequest request)
        {
            try
            {
                List<ProductCategorySummaryResponse> productCategories = await _filterProductCategoryUseCase.ExecuteAsync(request);
                return Ok(productCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductCategorySummaryResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                ProductCategorySummaryResponse productCategory = await _getProductCategoryUseCase.ExecuteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreateProductCategoryRequest request)
        {
            try
            {
                if (id != request.ProductCategoryId)
                {
                    return BadRequest("O ID da categoria de produto não corresponde ao ID fornecido na solicitação.");
                }
                await _updateProductCategoryUseCase.ExecuteAsync(request);
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
