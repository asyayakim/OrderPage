using ECommerceApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Order.Infrastructure.Persistence;

namespace ECommerce.Tests;

public class PostgreeConnectionTest
{
    private readonly AppDbContext _context;

    public PostgreeConnectionTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);
    }

    [Fact]
    public void TestDbConnection()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection")
                               ?? "Host=localhost;Database=ecommerce;Username=postgres;Password=12345678"; 


        optionsBuilder.UseNpgsql(connectionString);

        Assert.False(string.IsNullOrEmpty(connectionString));
    }
    [Fact]
    public void TestCollectionIfExist()
    {
        var newOrder = new ProductOrder(Guid.NewGuid());
        _context.Orders.Add(newOrder);
        _context.SaveChanges();

        var orders = _context.Orders;

        Assert.NotNull(orders);
        Assert.True(orders.Any());
    }
    
}