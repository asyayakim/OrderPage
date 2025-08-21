using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Order.Infrastructure.Persistence;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ECommerceApp.Api"))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        
        var connectionString = config.GetConnectionString("DefaultConnection") ??
                               Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection") ??
                               Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");
        optionsBuilder.UseNpgsql(connectionString, o => o.UseVector());

        return new AppDbContext(optionsBuilder.Options);
    }
}