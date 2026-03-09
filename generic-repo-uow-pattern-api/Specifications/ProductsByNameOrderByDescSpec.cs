using generic_repo_pattern_api.Entity;

namespace generic_repo_uow_pattern_api.Specifications
{
    public class ProductsByNameOrderByDescSpec : BaseSpecification<Product>
    {
        public ProductsByNameOrderByDescSpec(string name)
          : base(x => x.ProductName.Contains(name))
        {
            ApplyOrderByDescending(x => x.ProductName);
        }
    }
}