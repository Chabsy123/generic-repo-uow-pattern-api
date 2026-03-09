using generic_repo_pattern_api.Entity;

namespace generic_repo_uow_pattern_api.Specifications
{
    public class ProductsByNameOrderBySpec : BaseSpecification<Product>
    {
        public ProductsByNameOrderBySpec(string name)
            : base(x => x.ProductName.Contains(name))
        {
            ApplyOrderBy(x => x.ProductName);
            //ApplyPaging(1, 2);
            //AddInclude(x => x.Orders);
        }
    }
}