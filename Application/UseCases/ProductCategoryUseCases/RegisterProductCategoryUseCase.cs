using Application.Dtos.ProductCategory.Request;
using Application.Dtos.ProductCategory.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.ProductCategoryUseCases
{
    public class RegisterProductCategoryUseCase : IRegisterProductCategoryUseCase
    {

        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public RegisterProductCategoryUseCase(
            IProductCategoryRepository productCategoryRepository,
            ILogger<RegisterProductCategoryUseCase> logger,
            IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ProductCategorySummaryResponse> ExecuteAsync(CreateProductCategoryRequest request)
        {
            Name productCategoryName = new(request.ProductCategoryName);

            ProductCategory productCategory = new(productCategoryName, request.Description);

            _logger.LogInformation("Registering product category: {ProductCategoryName}", productCategoryName.Value);

            try
            {
                await _productCategoryRepository.AddAsync(productCategory);
            }
            catch (DuplicateEntryException ex)
            {
                _logger.LogError(ex, "Error updating index for product category: {ProductCategoryName}", productCategoryName.Value);
                throw new ArgumentException($"Error updating index for product category: {productCategoryName.Value}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering product category: {ProductCategoryName}", productCategoryName.Value);
                throw;
            }

            _logger.LogInformation("Product category registered successfully: {ProductCategoryName}", productCategoryName.Value);

            return _mapper.Map<ProductCategorySummaryResponse>(productCategory);
        }
    }
}
