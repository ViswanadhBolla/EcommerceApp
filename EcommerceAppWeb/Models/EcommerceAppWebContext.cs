using Microsoft.EntityFrameworkCore;

namespace EcommerceAppWeb.Models
{
    public class EcommerceAppWebContext:DbContext 
    {
        public EcommerceAppWebContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderedProduct>()
                .HasKey(op => new { op.ProductId, op.CustomerOrderId });
            

        }
        public EcommerceAppWebContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }        

        public DbSet<Product> Products { get; set; }

        public DbSet<CustomerOrder> CustomerOrders { get; set; }

        public DbSet<OrderedProduct> Orderedproducts { get; set; }

        public DbSet<Cart> Carts { get; set; }

        

    }
}
