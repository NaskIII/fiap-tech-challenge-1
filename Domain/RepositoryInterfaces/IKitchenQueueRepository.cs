using Domain.BaseInterfaces;
using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IKitchenQueueRepository : IRepository<KitchenQueue>
    {

        public Task<List<KitchenQueue>> ListKitchenQueue();
    }
}
