using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Infraestructure.BaseRepository;
using Infraestructure.DatabaseContext;
using Infraestructure.QueryExpressionBuilder.QueryExpresson;

namespace Infraestructure.Repositories
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ApplicationDatabaseContext context) : base(context)
        {
        }

        public Task<List<ProductCategory>> FilterProductCategory(string? name, string? description)
        {
            QueryExpression<ProductCategory> query = new();

            if (!string.IsNullOrEmpty(name))
            {
                Name newName = new(name);
                query.Where(pc => pc.ProductCategoryName == newName);
            }

            if (!string.IsNullOrEmpty(description))
            {
                query.Where(pc => pc.Description.Contains(description));
            }

            var predicate = query.Build();

            return this.GetManyAsync(predicate);
        }
    }
}
