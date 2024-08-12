using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.HasDefaultSchema("PRO");


            modelBuilder.Entity<Product>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CustomerId)
                .HasPrincipalKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, OrderNo = "ORDER00001", CustomerName = "Md. Monir Hossen", Phone = "01800000000", Address = "Panthapath, Dhaka", TotalOrderQty = 18, TotalOrderPrice = 3700 },
                new Customer { CustomerId = 2, OrderNo = "ORDER00002", CustomerName = "Md. Rakib Hasan", Phone = "01600000000", Address = "Mohammadpur, Dhaka", TotalOrderQty = 5, TotalOrderPrice = 25 },
                new Customer { CustomerId = 3, OrderNo = "ORDER00003", CustomerName = "Md. Rokon Islam", Phone = "01700000000", Address = "Mirpur, Dhaka", TotalOrderQty = 4, TotalOrderPrice = 110 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Book", Quantity = 5, UnitPrice = 100, TotalPrice = 500, CustomerId = 1 },
                new Product { ProductId = 2, ProductName = "Pen", Quantity = 10, UnitPrice = 20, TotalPrice = 200, CustomerId = 1 },
                new Product { ProductId = 3, ProductName = "Calculator", Quantity = 3, UnitPrice = 1000, TotalPrice = 3000, CustomerId = 1 },
                new Product { ProductId = 4, ProductName = "Sharpner", Quantity = 2, UnitPrice = 5, TotalPrice = 10, CustomerId = 2 },
                new Product { ProductId = 5, ProductName = "Eraser", Quantity = 3, UnitPrice = 5, TotalPrice = 15, CustomerId = 2 },
                new Product { ProductId = 6, ProductName = "Pencil", Quantity = 2, UnitPrice = 5, TotalPrice = 10, CustomerId = 3 },
                new Product { ProductId = 7, ProductName = "Marker", Quantity = 2, UnitPrice = 50, TotalPrice = 100, CustomerId = 3 }
                );
        }
    }
}
