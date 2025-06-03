using Application.Dtos.ProductDtos.Request;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.ProductUseCases
{
    public class RegisterProductUseCase : IRegisterProductUseCase
    {

        private readonly IProductRespository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public RegisterProductUseCase(
            IProductRespository productRepository, 
            ILogger<RegisterProductUseCase> logger,
            IMapper mapper,
            IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<Guid> ExecuteAsync(CreateProductRequest request)
        {
            ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.ProductCategoryId);
            if (productCategory == null) {
                _logger.LogError("Product category with ID {ProductCategoryId} not found.", request.ProductCategoryId);
                throw new NotFoundException($"Product category with ID {request.ProductCategoryId} not found.");
            }

            Name name = new(request.ProductName);

            Product product = new(name, request.Price, request.Description, productCategory);

            _logger.LogInformation("Registering product: {ProductName}", name.Value);

            await _productCategoryRepository.BeginTransactionAsync();

            try
            {
                await _productRepository.AddAsync(product);
            }
            catch (DuplicateEntryException ex)
            {
                _logger.LogError(ex, "Error updating index for product: {ProductName}", name.Value);
                await _productCategoryRepository.RollbackAsync();

                throw new ArgumentException($"Error updating index for product: {name.Value}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering product: {ProductName}", name.Value);
                await _productCategoryRepository.RollbackAsync();

                throw;
            }

            await _productCategoryRepository.CommitTransactionAsync();

            _logger.LogInformation("Product registered successfully: {ProductName}", name.Value);

            return product.ProductId;
        }
    }
}
