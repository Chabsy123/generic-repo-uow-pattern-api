using generic_repo_pattern_api.Entity;

namespace generic_repo_uow_pattern_api.Specifications
{
    public class ProductByNameSpec : BaseSpecification<Product>
    {
        public ProductByNameSpec(string productName)
            : base(x => x.ProductName.Contains(productName))
        {

        }
    }
}