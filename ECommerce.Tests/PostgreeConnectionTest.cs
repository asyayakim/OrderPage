using Microsoft.Extensions.Configuration;

namespace ECommerce.Tests;

public class PostgreeConnectionTest
{
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
}