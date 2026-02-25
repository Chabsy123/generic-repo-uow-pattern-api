using generic_repo_pattern_api.Data;
using generic_repo_pattern_api.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace generic_repo_uow_pattern_api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _myDbContext;
        private readonly Dictionary<Type, object> repositories;
        private IDbContextTransaction _transaction;

        public UnitOfWork(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
            repositories = new Dictionary<Type, object>();
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _myDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null!;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _myDbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (repositories.ContainsKey(typeof(T)))
            {
                return repositories[typeof(T)] as IRepository<T>;
            }
            var repository = new Repository<T>(_myDbContext);
            repositories.Add(typeof(T), repository);
            return repository;
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _myDbContext.SaveChangesAsync();
        }
    }
}
