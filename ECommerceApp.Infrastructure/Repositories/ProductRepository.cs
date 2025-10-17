
using ECommerceApp.Domain;
using ECommerceApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProductsFromDb(int pageNumber, int pageSize)
    {
        return await _context.Products
            .Include(p => p.Store)
            .Include(p => p.Nutrition)
            .OrderBy(p => p.ProductName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Product?> GetProductById(Guid id)
    {
        return await _context.Products .AsNoTracking()
            .Include(p => p.Store)
            .Include(p => p.Nutrition)
            .FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<Product?> GetOneAsync()
    {
        var product = await _context.Products.FirstOrDefaultAsync();
        return product;
    }
}
