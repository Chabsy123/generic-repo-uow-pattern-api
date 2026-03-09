using generic_repo_pattern_api.Entity;

namespace generic_repo_uow_pattern_api.Specifications
{
    public class ProductsByNameOrderByPagingSpec : BaseSpecification<Product>
    {
        public ProductsByNameOrderByPagingSpec(string name, int pageNumber, int pageSize)
           : base(x => x.ProductName.Contains(name))
        {
            ApplyOrderBy(x => x.ProductName);
            ApplyPaging(pageNumber, pageSize);

        }
    }
}