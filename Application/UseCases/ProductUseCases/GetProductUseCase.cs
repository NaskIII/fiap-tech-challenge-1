using Application.Dtos.ProductDtos.Response;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.UseCases.ProductUseCases
{
    public class GetProductUseCase : IGetProductUseCase
    {
        private readonly IProductRespository _productRepository;
        private readonly IMapper _mapper;

        public GetProductUseCase(IProductRespository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductSummaryResponse> ExecuteAsync(Guid request)
        {
            Product? product = await _productRepository.GetByIdAsync(request);
            if (product == null)
                throw new NotFoundException($"Product with ID {request} not found.");

            return _mapper.Map<ProductSummaryResponse>(product);
        }
    }
}
