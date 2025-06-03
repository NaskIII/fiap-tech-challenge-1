using Application.Dtos.ProductCategory.Response;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.UseCases.ProductCategoryUseCases
{
    public class GetProductCategoryUseCase : IGetProductCategoryUseCase
    {

        private readonly IProductCategoryRepository _productCategoryRepository;
        private IMapper _mapper;

        public GetProductCategoryUseCase(IProductCategoryRepository productCategoryRepository, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        public async Task<ProductCategorySummaryResponse> ExecuteAsync(Guid request)
        {
            ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request);
            if (productCategory == null)
                throw new NotFoundException("Categoria de produto não encontrada.");

            return _mapper.Map<ProductCategorySummaryResponse>(productCategory);
        }
    }
}
