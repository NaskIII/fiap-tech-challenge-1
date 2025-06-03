using Domain.BaseInterfaces;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {

        public Task<List<User>> FilterUserAsync(string? userName, string? email);
        public Task<User?> GetUserByEmail(Email email);
    }
}
