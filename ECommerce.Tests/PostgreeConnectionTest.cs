using Microsoft.Extensions.Configuration;
using Order.Infrastructure.Persistence;

namespace ECommerce.Tests;

public class PostgreeConnectionTest
{
    private readonly AppDbContext _context;

    public PostgreeConnectionTest(AppDbContext context)
    {
        _context = context;
    }

    [Fact]
    public void TestDbConnection()
    {
        //arrange
        //act
        //assert
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .Build();
        var connectionString = config.GetConnectionString("DefaultConnection")
                               ?? "Host=localhost;Database=ecommerce;Username=postgres;Password=12345678"; 

        Assert.NotEmpty(connectionString);
        Assert.True(!string.IsNullOrEmpty(connectionString));
    }
    [Fact]
    public void TestCollectionIfExist()
    {
        //arrange
        var newOrder = new ECommerceApp.Domain.ProductOrder(Guid.NewGuid());
        _context.Orders.Add(newOrder);

        _context.SaveChanges();
        // Act
        var orders = _context.Orders;

        // Assert
        Assert.NotNull(orders);
        Assert.True(orders.Any());;
    }
}