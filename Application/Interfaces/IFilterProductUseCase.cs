using Application.Dtos.ProductDtos.Request;
using Application.Dtos.ProductDtos.Response;

namespace Application.Interfaces
{
    public interface IFilterProductUseCase : IUseCase<ProductFilterRequest, List<ProductSummaryResponse>>
    {
    }
}
