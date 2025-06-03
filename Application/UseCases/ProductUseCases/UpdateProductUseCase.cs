using Application.Dtos.ProductDtos.Request;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.ProductUseCases
{
    public class UpdateProductUseCase : IUpdateProductUseCase
    {

        private readonly IProductRespository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ILogger _logger;

        public UpdateProductUseCase(
            IProductRespository productRepository,
            IProductCategoryRepository productCategoryRepository,
            ILogger<UpdateProductUseCase> logger)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _logger = logger;
        }

        public async Task ExecuteAsync(CreateProductRequest request)
        {
            if (request.ProductId == null)
            {
                _logger.LogError("Product ID cannot be empty.");
                throw new ArgumentException("Product ID cannot be empty.");
            }

            ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.ProductCategoryId);
            if (productCategory == null)
            {
                _logger.LogError("Product category with ID {ProductCategoryId} not found.", request.ProductCategoryId);
                throw new NotFoundException($"Product category with ID {request.ProductCategoryId} not found.");
            }

            Product? product = await _productRepository.GetByIdAsync(request.ProductId.Value);
            if (product == null)
            {
                _logger.LogError("Product with ID {ProductId} not found.", request.ProductId);
                throw new NotFoundException($"Product with ID {request.ProductId} not found.");
            }

            _logger.LogInformation("Updating product with ID: {ProductId}", request.ProductId);

            product.Update(
                new Name(request.ProductName),
                request.Description,
                request.Price,
                productCategory
            );

            await _productCategoryRepository.BeginTransactionAsync();

            try
            {
                await _productRepository.UpdateAsync(product);
            }
            catch (DuplicateEntryException ex)
            {
                _logger.LogError(ex, "Error updating product with ID: {ProductId}. Product already exists.", request.ProductId);
                await _productCategoryRepository.RollbackAsync();

                throw new ArgumentException("Product already exists.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID: {ProductId}", request.ProductId);
                await _productCategoryRepository.RollbackAsync();

                throw new ApplicationException("Error updating the product.", ex);
            }

            await _productCategoryRepository.CommitTransactionAsync();

            _logger.LogInformation("Product with ID: {ProductId} updated successfully.", request.ProductId);
        }
    }
}
