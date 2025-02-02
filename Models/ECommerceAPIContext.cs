using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Models
{
    public class ECommerceAPIContext : DbContext
    {
        public ECommerceAPIContext(DbContextOptions<ECommerceAPIContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}
