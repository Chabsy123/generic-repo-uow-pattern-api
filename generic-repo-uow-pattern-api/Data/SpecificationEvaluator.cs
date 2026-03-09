using generic_repo_uow_pattern_api.Repository;
using Microsoft.EntityFrameworkCore;

namespace generic_repo_uow_pattern_api.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : class //baseentity
    {
        public static IQueryable<TEntity> GetQuery(
            IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            query = specification.Includes.Aggregate(query,
                                   (current, include) => current.Include(include));

            query = specification.IncludeStrings.Aggregate(query,
                                    (current, include) => current.Include(include));
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }
            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip.Value)
                             .Take(specification.Take.Value);
            }
            return query;
        }
    }
}
