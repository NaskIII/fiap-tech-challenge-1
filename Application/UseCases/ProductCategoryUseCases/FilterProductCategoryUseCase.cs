using Application.Dtos.ProductCategory.Request;
using Application.Dtos.ProductCategory.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.UseCases.ProductCategoryUseCases
{
    public class FilterProductCategoryUseCase : IFilterProductCategoryUseCase
    {

        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        public FilterProductCategoryUseCase(IProductCategoryRepository productCategoryRepository, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductCategorySummaryResponse>> ExecuteAsync(FilterProductCategoryRequest request)
        {
            List<ProductCategory> productCategories = await _productCategoryRepository.FilterProductCategory(request.ProductCategoryName, request.Description);

            return _mapper.Map<List<ProductCategorySummaryResponse>>(productCategories);
        }
    }
}
