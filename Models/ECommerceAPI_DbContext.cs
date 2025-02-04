using Microsoft.EntityFrameworkCore;
using E_Commerce_API.Models;

namespace E_Commerce_API.Models
{
    public class ECommerceAPI_DbContext : DbContext
    {
        public ECommerceAPI_DbContext(DbContextOptions<ECommerceAPI_DbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}
