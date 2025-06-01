using Domain.BaseInterfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Infraestructure.DatabaseContext;
using Infraestructure.Exceptions;

namespace Infraestructure.Repositories
{
    internal class Repository<T> : ReadonlyRepository<T>, IRepository<T> where T : class
    {
        private IDbContextTransaction? _transaction;
        private bool _transactionOpened;

        public Repository(ApplicationDatabaseContext context) : base(context)
        {
            _transactionOpened = false;
        }

        public bool TransactionOpened => _transactionOpened;

        public void AddToContext(T entity)
        {
            try
            {
                _ = DbSet.Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the entity to the context.", ex);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var rowsAffected = await Context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("An error occurred while saving changes to the database.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving changes.", ex);
            }
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                _ = DbSet.Add(entity);
                var rowsAffected = await Context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("An error occurred while adding the entity to the database.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task BeginTransactionAsync()
        {
            try
            {
                _transaction = await Context.Database.BeginTransactionAsync();
                _transactionOpened = true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while starting the transaction.", ex);
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (_transaction == null)
                {
                    throw new TransactionIsNotOpen("A transação do banco de dados não foi aberta. Certifique-se de abrir a transação antes de realizar operações de banco de dados.");
                }

                _transaction.Commit();
                _transactionOpened = false;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while committing the transaction.", ex);
            }
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                var entry = Context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
                entry.State = EntityState.Deleted;
                var rowsAffected = await Context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("An error occurred while deleting the entity from the database.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction == null)
                {
                    throw new TransactionIsNotOpen("A transação do banco de dados não foi aberta. Certifique-se de abrir a transação antes de realizar operações de banco de dados.");
                }

                _transaction.Rollback();
                _transactionOpened = false;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while rolling back the transaction.", ex);
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                var entry = Context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }

                entry.State = EntityState.Modified;

                var rowsAffected = await Context.SaveChangesAsync();

                return rowsAffected > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Concurrency conflict occurred while updating the entity.");
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("An error occurred while updating the entity in the database.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }
        }

        public void AddToSet(T entity)
        {
            DbSet.Add(entity);
        }
    }
}
