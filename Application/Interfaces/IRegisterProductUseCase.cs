using Application.Dtos.ProductDtos.Request;

namespace Application.Interfaces
{
    public interface IRegisterProductUseCase : IUseCase<CreateProductRequest, Guid>
    {
    }
}
