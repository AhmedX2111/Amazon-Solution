using Amazon.Models;
using Amazon.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Amazon.Data
{
	// Inherit 7 dbsets from Identity                          // Generic overload
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>    /*<IdentityUser, IdentityRole, string>*/
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Laptop>()
			.Property(l => l.Price)
			.HasColumnType("decimal(18,2)"); // Specify precision and scale

            builder.Entity<Laptop>()
					.Ignore(l => l.FormFile);

            builder.Entity<Laptop>()
            .Property(l => l.Images)
            .HasColumnType("nvarchar(max)"); // Use a suitable type for storing JSON strings

            /*builder.Entity<IdentityUser>();
			builder.Entity<IdentityRole>();
			builder.Entity<IdentityUserRole<string>>();*/
        }
		public DbSet<Laptop> Laptops { get; set; }
		public DbSet<Customer> Customers { get; set; }
		
		/*public DbSet<IdentityUser> Users { get; set; }
		public DbSet<IdentityRole> Roles {  get; set; }*/



	}
}
