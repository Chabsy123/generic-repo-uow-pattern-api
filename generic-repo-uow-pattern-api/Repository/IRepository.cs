using generic_repo_pattern_api.Data;
using generic_repo_uow_pattern_api.Repository;
using System.Linq.Expressions;

namespace generic_repo_pattern_api.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void SetDbContext(MyDbContext myDbcontext);
        Task AddRangeAsync(IEnumerable<T> entities);

        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<(ICollection<T> Result, int TotalNumber, int TotalPages,
            bool IsPrevious, bool IsNext)> SearchOrderAndPaginateAsync(
      Expression<Func<T, bool>> searchPredicate = null,
      Expression<Func<T, object>> orderBy = null,
      bool isDescending = false,
      int? pageNumber = null,
      int? pageSize = null,
      params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification = null);
        Task<T> FindAsync(ISpecification<T> specification = null);

    }
}
