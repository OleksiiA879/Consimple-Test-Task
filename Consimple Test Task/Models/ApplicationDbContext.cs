using Microsoft.EntityFrameworkCore;

namespace Consimple_Test_Task.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .HasKey(pp => new { pp.OrderId, pp.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(pp => pp.Order)
                .WithMany(p => p.PurchaseProducts)
                .HasForeignKey(pp => pp.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.PurchaseProducts)
                .HasForeignKey(pp => pp.ProductId);
        }
    }
}
