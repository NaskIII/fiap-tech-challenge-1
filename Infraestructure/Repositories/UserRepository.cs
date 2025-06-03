using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Infraestructure.BaseRepository;
using Infraestructure.DatabaseContext;
using Infraestructure.QueryExpressionBuilder.QueryExpresson;

namespace Infraestructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDatabaseContext context) : base(context)
        {
        }

        public async Task<List<User>> FilterUserAsync(string? userName, string? email)
        {
            QueryExpression<User> queryBuilder = new();

            if (!string.IsNullOrEmpty(userName))
                queryBuilder.Where(c => c.UserName.Contains(userName));

            if (!string.IsNullOrEmpty(email))
            {
                Email newEmail = new(email);
                queryBuilder.Where(c => c.Email == newEmail);
            }

            var predicate = queryBuilder.Build();

            return await this.GetManyAsync(predicate);
        }

        public async Task<User?> GetUserByEmail(Email email)
        {
            QueryExpression<User> queryBuilder = new();

            queryBuilder.Where(c => c.Email == email);
            var predicate = queryBuilder.Build();

            return await this.GetSingleAsync(predicate);
        }
    }
}
