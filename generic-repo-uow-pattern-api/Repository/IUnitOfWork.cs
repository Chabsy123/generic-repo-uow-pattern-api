using generic_repo_pattern_api.Repository;

namespace generic_repo_uow_pattern_api.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();

        Task CommitAsync();
        Task RollbackAsync();

        public IProductRepository ProductRepository { get; }

        TRepository GetRepository<TRepository, TEntity>() where TRepository : class,
           IRepository<TEntity> where TEntity : class;
    }
}

