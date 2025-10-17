using ECommerceApp.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<UserData, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions
        <AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Nutrition> Nutritions { get; set; }
    public DbSet<ProductOrder> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<UserData> UsersData { get; set; }
    public DbSet<EmbeddingEntry> Embeddings { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension("vector");
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Address)
            .WithOne(a => a.Customer)
            .HasForeignKey<Address>(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
       
        modelBuilder.Entity<EmbeddingEntry>(entity =>
        {
            entity.ToTable("embeddings"); 
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TextContent).HasColumnName("text_content");
            entity.Property(e => e.Embedding)
                .HasColumnName("embedding")
                .HasColumnType("vector");
            entity.Property(e => e.Metadata).HasColumnName("metadata");
        });  
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Nutrition)
            .WithOne(n => n.Product)
            .HasForeignKey(n => n.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}