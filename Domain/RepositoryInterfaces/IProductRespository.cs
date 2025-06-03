using Domain.BaseInterfaces;
using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IProductRespository : IRepository<Product>
    {

        public Task<List<Product>> FilterProductsAsync(string? productName, string? description, decimal? price, Guid? productCategoryId);
    }
}
