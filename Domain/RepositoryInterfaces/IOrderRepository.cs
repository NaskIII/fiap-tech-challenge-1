using Domain.BaseInterfaces;
using Domain.Entities;
using Domain.Enums;

namespace Domain.RepositoryInterfaces
{
    public interface IOrderRepository : IRepository<Order>
    {

        public Task<List<Order>> FilterOrders(DateTime? orderDate, Guid? clientId, OrderStatus? orderStatus);
    }
}
