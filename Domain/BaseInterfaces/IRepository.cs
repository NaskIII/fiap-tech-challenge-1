namespace Domain.BaseInterfaces
{
    public interface IRepository<T> : IReadonlyRepository<T> where T : class
    {
        Task BeginTransactionAsync();
        void CommitTransaction();
        void Rollback();
        Task<bool> AddAsync(T entity);
        void AddToSet(T entity);
        Task<bool> SaveChangesAsync();
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        bool TransactionOpened { get; }
    }
}
