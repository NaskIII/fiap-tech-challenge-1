using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Infraestructure.BaseRepository;
using Infraestructure.DatabaseContext;
using Infraestructure.QueryExpressionBuilder.QueryExpresson;

namespace Infraestructure.Repositories
{
    public class ProductRespository : Repository<Product>, IProductRespository

    {
        public ProductRespository(ApplicationDatabaseContext context) : base(context)
        {
        }

        public Task<List<Product>> FilterProductsAsync(
            string? productName,
            string? description,
            decimal? price,
            Guid? productCategoryId)
        {
            QueryExpression<Product> query = new();

            if (!string.IsNullOrEmpty(productName))
            {
                Name newName = new(productName); 
                query = query.Where(p => p.ProductName == newName);
            }

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(p => p.Description.Contains(description));
            }

            if (price.HasValue)
            {
                query = query.Where(p => p.Price == price.Value);
            }

            if (productCategoryId.HasValue)
            {
                query = query.Where(p => p.ProductCategoryId == productCategoryId.Value);
            }

            var predicae = query.Build();

            return this.GetManyAsync(predicae, x => x.ProductCategory);
        }
    }
}
