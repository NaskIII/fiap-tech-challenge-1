using Application.Dtos.ProductCategory.Response;

namespace Application.Interfaces
{
    public interface IGetProductCategoryUseCase : IUseCase<Guid, ProductCategorySummaryResponse>
    {
    }
}
