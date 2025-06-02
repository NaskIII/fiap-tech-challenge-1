using Domain.Entities;
using Domain.RepositoryInterfaces;
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

        public async Task<List<Client>> FilterClientAsync(string? name, string? cpf, string? email) // TODO: Ajustar filtro por CPF, o EFCore não consegue traduzir.
        {
            QueryExpression<Client> queryBuilder = new();

            if (!string.IsNullOrEmpty(name))
                queryBuilder.Where(c => c.Name.Contains(name));

            if (!string.IsNullOrEmpty(cpf))
                queryBuilder.Where(c => c.CPF.Value == cpf);

            if (!string.IsNullOrEmpty(email))
                queryBuilder.Where(c => c.Email.Value == email);

            var predicate = queryBuilder.Build();

            return await this.GetManyAsync(predicate);
        }

        public async Task<Client?> GetClientByCpfAsync(string cpf) // TODO: Ajustar filtro por CPF, o EFCore não consegue traduzir.
        {
            QueryExpression<Client> queryBuilder = new();

            queryBuilder.Where(x => x.CPF.Value == cpf);

            var predicate = queryBuilder.Build();

            return await this.GetSingleAsync(predicate);
        }
    }
}
