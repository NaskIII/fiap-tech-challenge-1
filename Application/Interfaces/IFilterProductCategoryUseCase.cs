using Application.Dtos.ProductCategory.Request;
using Application.Dtos.ProductCategory.Response;

namespace Application.Interfaces
{
    public interface IFilterProductCategoryUseCase : IUseCase<FilterProductCategoryRequest, List<ProductCategorySummaryResponse>>
    {
    }
}
