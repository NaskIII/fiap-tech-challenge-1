using Domain.BaseInterfaces;
using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {

        public Task<List<ProductCategory>> FilterProductCategory(string? name, string? description);
    }
}
