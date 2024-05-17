using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;


namespace Context.Context
{
	public class Customer : IdentityUser
	{
		public List<Order> Orders;
	}
	public class ECommerceWebsiteContext : IdentityDbContext<Customer>
	{
		public ECommerceWebsiteContext(DbContextOptions<ECommerceWebsiteContext> options) : base(options) { }

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<ProductOrder> ProductOrders { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			//Product
			builder.Entity<Product>(Product =>
			{
				Product.Property(p => p.ProductName).IsRequired();
				Product.Property(p => p.ProductStatus).HasDefaultValue("Created");

				Product.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryID)
				.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<Category>(Category =>
			{
				Category.Property(p => p.CategoryName).IsRequired();
			});

			builder.Entity<Order>(Order =>
			{
				Order.Property(o => o.OrderStatus).HasDefaultValue("Success");

				Order.HasOne(o => o.Customer)
				.WithMany(c => c.Orders)
				.HasForeignKey(o => o.CustomerID)
				.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<ProductOrder>(ProductOrder =>
			{
				ProductOrder.HasKey(po => new
				{
					po.ProductID,
					po.OrderID
				});
				ProductOrder.HasOne(po => po.Product)
				.WithMany(t => t.Products)
				.OnDelete(DeleteBehavior.Cascade);

				ProductOrder.HasOne(po => po.Order)
				.WithMany(p => p.Products)
				.HasForeignKey(op => op.OrderID)
				.OnDelete(DeleteBehavior.Cascade);

			});

			base.OnModelCreating(builder);
		}

	}
}
