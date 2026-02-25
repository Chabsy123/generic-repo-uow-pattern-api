using generic_repo_pattern_api.Entity;
using Microsoft.EntityFrameworkCore;

namespace generic_repo_pattern_api.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public MyDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .HasOne(o => o.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.ProductId);
            
        }
    }
}
