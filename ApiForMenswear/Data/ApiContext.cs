using ApiForMenswear.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiForMenswear.Data
{
	public class ApiContext : DbContext
	{
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            
        }

        public DbSet<UserSignup> UserSignup { get; set; }
        public DbSet<SellerSignup> SellerSignup { get; set; }
        public DbSet<ProductManagement> ProductManagement { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Category> Category { get; set; }

		
	}

}
