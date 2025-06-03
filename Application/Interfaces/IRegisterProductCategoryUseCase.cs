using Application.Dtos.ProductCategory.Request;
using Application.Dtos.ProductCategory.Response;

namespace Application.Interfaces
{
    public interface IRegisterProductCategoryUseCase : IUseCase<CreateProductCategoryRequest, ProductCategorySummaryResponse>
    {
    }
}
