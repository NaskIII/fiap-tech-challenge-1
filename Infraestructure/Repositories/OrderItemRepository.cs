using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infraestructure.BaseRepository;
using Infraestructure.DatabaseContext;

namespace Infraestructure.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDatabaseContext context) : base(context)
        {
        }
    }
}
