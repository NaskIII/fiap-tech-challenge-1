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

        public async Task<List<Client>> FilterClientAsync(string? name, string? cpf, string? email)
        {
            QueryExpression<Client> queryBuilder = new();

            if (!string.IsNullOrEmpty(name))
                queryBuilder.And(c => c.Name.Contains(name));

            if (!string.IsNullOrEmpty(cpf))
                queryBuilder.And(c => c.CPF.Value == cpf);

            if (!string.IsNullOrEmpty(email))
                queryBuilder.And(c => c.Email.Value == email);

            var predicate = queryBuilder.Build();

            return await this.GetManyAsync(predicate);
        }
    }
}
