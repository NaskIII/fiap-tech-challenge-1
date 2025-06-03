using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Infraestructure.BaseRepository;
using Infraestructure.DatabaseContext;
using Infraestructure.QueryExpressionBuilder.QueryExpresson;

namespace Infraestructure.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDatabaseContext context) : base(context)
        {
        }

        public async Task<List<Client>> FilterClientAsync(string? name, string? cpf, string? email)
        {
            QueryExpression<Client> queryBuilder = new();

            if (!string.IsNullOrEmpty(name))
                queryBuilder.Where(c => c.Name.Contains(name));

            if (!string.IsNullOrEmpty(cpf))
            {
                Cpf newCpf = new(cpf);
                queryBuilder.Where(c => c.CPF == newCpf);
            }

            if (!string.IsNullOrEmpty(email))
            {
                Email newEmail = new(email);
                queryBuilder.Where(c => c.Email == newEmail);
            }

            var predicate = queryBuilder.Build();

            return await this.GetManyAsync(predicate);
        }

        public async Task<Client?> GetClientByCpfAsync(string cpf)
        {
            QueryExpression<Client> queryBuilder = new();

            Cpf newCpf = new(cpf);
            queryBuilder.Where(x => x.CPF == newCpf);

            var predicate = queryBuilder.Build();

            return await this.GetSingleAsync(predicate);
        }
    }
}
