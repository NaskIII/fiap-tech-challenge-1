using Domain.Entities;
using Domain.Enums;
using Domain.RepositoryInterfaces;
using Infraestructure.BaseRepository;
using Infraestructure.DatabaseContext;
using Infraestructure.QueryExpressionBuilder.QueryExpresson;

namespace Infraestructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDatabaseContext context) : base(context)
        {
        }

        public Task<List<Order>> FilterOrders(DateTime? orderDate, Guid? clientId, OrderStatus? orderStatus)
        {
            QueryExpression<Order> query = new();

            if (orderDate != null)
                query.Where(o => o.OrderDate.Date == orderDate.Value.Date);

            if (clientId != null)
                query.Where(o => o.ClientId == clientId.Value);

            if (orderStatus != null)
                query.Where(o => o.OrderStatus == orderStatus.Value);

            var predicate = query.Build();

#pragma warning disable CS8603 // Possível retorno de referência nula.
            return this.GetManyAsync(predicate,
                "OrderItems.Product",
                "Client");
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }
    }
}
