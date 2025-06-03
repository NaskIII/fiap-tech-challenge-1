using Application.Dtos.ProductDtos.Response;

namespace Application.Interfaces
{
    public interface IGetProductUseCase : IUseCase<Guid, ProductSummaryResponse>
    {
    }
}
