using Application.Dtos.ProductCategory.Request;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.ProductCategoryUseCases
{
    public class UpdateProductCategoryUseCase : IUpdateProductCategoryUseCase
    {

        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateProductCategoryUseCase(
            IProductCategoryRepository productCategoryRepository,
            IMapper mapper,
            ILogger<UpdateProductCategoryUseCase> logger)
        {
            _productCategoryRepository = productCategoryRepository ?? throw new ArgumentNullException(nameof(productCategoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        public async Task ExecuteAsync(CreateProductCategoryRequest request)
        {
            if (request.ProductCategoryId == null)
            {
                throw new ArgumentException("ProductCategoryId is required.", nameof(request.ProductCategoryId));
            }

            ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.ProductCategoryId.Value);
            if (productCategory == null)
                throw new NotFoundException("A Categoria de Produto solicitada não foi encontrada.");

            _logger.LogInformation("Atualizando categoria de produto com ID: {ProductCategoryId}", request.ProductCategoryId);

            Name newName = new(request.ProductCategoryName);
            productCategory.UpdateName(newName);
            productCategory.UpdateDescription(request.Description);

            try
            {
                await _productCategoryRepository.UpdateAsync(productCategory);
                _logger.LogInformation("Categoria de produto com ID: {ProductCategoryId} atualizada com sucesso.", request.ProductCategoryId);
            }
            catch (DuplicateEntryException ex)
            {
                _logger.LogError(ex, "Erro ao atualizar categoria de produto com ID: {ProductCategoryId}. Categoria já existe.", request.ProductCategoryId);
                throw new ArgumentException("Categoria de produto já existe.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar categoria de produto com ID: {ProductCategoryId}", request.ProductCategoryId);
                throw new ApplicationException("Erro ao atualizar a categoria de produto.", ex);
            }
        }
    }
}
