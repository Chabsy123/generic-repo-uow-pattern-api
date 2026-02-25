namespace generic_repo_pattern_api.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
