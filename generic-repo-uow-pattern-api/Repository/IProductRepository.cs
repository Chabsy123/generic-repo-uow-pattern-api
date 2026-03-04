using generic_repo_pattern_api.Entity;
using generic_repo_uow_pattern_api.Common;

namespace generic_repo_uow_pattern_api.Repository
{
    public interface IProductRepository : generic_repo_pattern_api.Repository.IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByName(string productName);
        Task<PaginatedList<Product>> GetAllProductsWithPagging(int page, int pageSize, string searchTerm);
    } 
}
