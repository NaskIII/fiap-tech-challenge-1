using Domain.Entities;
using Domain.Enums;
using Domain.RepositoryInterfaces;
using Infraestructure.BaseRepository;
using Infraestructure.DatabaseContext;
using Infraestructure.QueryExpressionBuilder.QueryExpresson;

namespace Infraestructure.Repositories
{
    public class KitchenQueueRepository : Repository<KitchenQueue>, IKitchenQueueRepository
    {
        public KitchenQueueRepository(ApplicationDatabaseContext context) : base(context)
        {
        }

        public async Task<List<KitchenQueue>> ListKitchenQueue()
        {
            QueryExpression<KitchenQueue> queryExpression = new();

            queryExpression.Where(x => x.Order.OrderStatus != OrderStatus.Completed);
            queryExpression.Where(x => x.Order.OrderStatus != OrderStatus.Canceled);
            queryExpression.Where(x => x.Order.OrderStatus != OrderStatus.Pending);
            queryExpression.Where(x => x.Order.OrderStatus != OrderStatus.Completed);
            
            var predicate = queryExpression.Build();

#pragma warning disable CS8603 // Possível retorno de referência nula.
            List<KitchenQueue> kitchenQueues = await GetManyAsync(predicate, x => x.Order.Client);
#pragma warning restore CS8603 // Possível retorno de referência nula.

            return kitchenQueues.OrderBy(x => x.Order.OrderDate).ToList();
        }
    }
}
