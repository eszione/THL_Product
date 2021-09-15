using Microsoft.EntityFrameworkCore;
using Product.Types.Models;

namespace Product.Repositories.Implementations
{
    public class ProductContext : DbContext 
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<ProductRecord> Products { get; set; }
    }
}
