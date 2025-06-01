using ECommerceApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions
        <AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Product> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    

}