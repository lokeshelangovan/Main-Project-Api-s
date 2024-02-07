using ApiForMenswear.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiForMenswear.Data
{
	public class MenswearContext : DbContext
	{
        public MenswearContext(DbContextOptions<MenswearContext> options) : base(options)
        {
            
        }

        public DbSet<UserSignup> UserSignup { get; set; }
        public DbSet<SellerSignup> SellerSignup { get; set; }
        public DbSet<ProductManagement> ProductManagement { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
           

        //    modelBuilder.Entity<OrderItem>()
        //   .HasOne<Order>()
        //   .WithMany()
        //   .HasForeignKey(o => o.OrderId);

        //    modelBuilder.Entity<OrderItem>()
        //  .HasOne<ProductManagement>()
        //  .WithMany()
        //  .HasForeignKey(o => o.ProductId);

        //    modelBuilder.Entity<ShoppingCart>()
        //  .HasOne<UserSignup>()
        //  .WithMany()
        //  .HasForeignKey(u => u.UserId);

        //    modelBuilder.Entity<ShoppingCart>()
        //  .HasOne<ProductManagement>()
        //  .WithMany()
        //  .HasForeignKey(s => s.ProductId);


        //}

    }

}
