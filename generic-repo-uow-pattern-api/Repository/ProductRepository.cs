using generic_repo_pattern_api.Data;
using generic_repo_pattern_api.Entity;
using generic_repo_pattern_api.Repository;
using generic_repo_uow_pattern_api.Common;
using Microsoft.EntityFrameworkCore;

namespace generic_repo_uow_pattern_api.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext myDbContext) : base(myDbContext){ }

        public async Task<PaginatedList<Product>> GetAllProductsWithPagging(int page, int pageSize, string searchTerm)
        {
            IQueryable<Product> query = _dbSet
                .Include(p => p.Orders); //includes the related orders in the query

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => EF.Functions.Like(p.ProductName, $"%{searchTerm}%"));
            }

            var Result = await PaginatedList<Product>.
                ToPagedList(query.OrderBy(x => x.ProductId), page, pageSize);
            return Result;

        }

        public async Task<IEnumerable<Product>> GetProductsByName(string productName)
        {
            return await _dbSet.Where(p => p.ProductName.Contains(productName)).ToListAsync();
        }
    }
}
