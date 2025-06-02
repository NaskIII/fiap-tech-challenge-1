using Domain.BaseInterfaces;
using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<List<Client>> FilterClientAsync(string? name, string? cpf, string? email);
        Task<Client?> GetClientByCpfAsync(string cpf);

    }
}
