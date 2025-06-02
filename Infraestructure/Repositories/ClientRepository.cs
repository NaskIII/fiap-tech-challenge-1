using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infraestructure.BaseRepository;
using Infraestructure.DatabaseContext;

namespace Infraestructure.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDatabaseContext context) : base(context)
        {
        }
    }
}
