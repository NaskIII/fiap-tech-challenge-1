using Application.Dtos.ProductDtos.Request;
using Application.Dtos.ProductDtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.UseCases.ProductUseCases
{
    public class FilterProductUseCase : IFilterProductUseCase
    {

        private readonly IProductRespository _productRepository;
        private readonly IMapper _mapper;

        public FilterProductUseCase(
            IProductRespository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductSummaryResponse>> ExecuteAsync(ProductFilterRequest request)
        {
            List<Product> products = await _productRepository.FilterProductsAsync(request.ProductName, request.Description, request.Price, request.ProductCategoryId);

            return _mapper.Map<List<ProductSummaryResponse>>(products);
        }
    }
}
