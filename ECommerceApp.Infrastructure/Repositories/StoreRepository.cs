using Microsoft.EntityFrameworkCore;
using ECommerceApp.Domain;
using ECommerceApp.Domain.Interfaces;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class StoreRepository : IStoreRepository
{
    private readonly AppDbContext _context;

    public StoreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Store>> GetAllAsync()
    {
        return await _context.Stores.ToListAsync();
    }

    public async Task<List<Product?>> GetAllProductsByStoreIdAsync(Guid storeId)
    {
        return (await _context.Products
            .Include(p => p.Store)
            .Include(p => p.Nutrition)
            .Where(p => p.Store.StoreId == storeId)
            .ToListAsync())!;
    }

    public async Task<List<Product?>> GetAllTopSellersAsync()
    {
        return (await _context.Products
            .Include(p => p.Store)
            .Include(p => p.Nutrition)
            .Take(4).ToListAsync())!;
    }

    public async Task<List<string>> GetAllCategoriesAsync()
    {
        return await _context.Products
            .Where(p => !string.IsNullOrEmpty(p.Category))
            .Select(p => p.Category)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Product>> GetAllByCategoryAsync(string? category, int pageNumber, int pageSize,
        decimal? minPrice, decimal? maxPrice, string? sortBy)
    {
        var query =  _context.Products
            .Include(p => p.Store)
            .Include(p => p.Nutrition)
            .AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Category == category);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.UnitPrice >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.UnitPrice <= maxPrice.Value);
        }

        query = sortBy switch
        {
            "price_asc" => query.OrderBy(p => p.UnitPrice),
            "price_desc" => query.OrderByDescending(p => p.UnitPrice),
            "name_asc" => query.OrderBy(p => p.ProductName),
            "name_desc" => query.OrderByDescending(p => p.ProductName),
            _ => query.OrderByDescending(p => p.ProductId)
        };

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Product>> GetAllFeaturedAsync(int limit)
    {
        return await _context.Products.Include(p => p.Store)
            .Include(p => p.Nutrition)
            .Take(limit)
            .OrderBy(p => p.UnitPrice)
            .ToListAsync();
    }

    public async Task<Dictionary<string, string>> GetImageUrlByCategoryName(List<string> categories)
    {
        var imageUrl = await _context.Products
            .Where(p => !string.IsNullOrEmpty(p.Category) && categories.Contains(p.Category))
            .GroupBy(p => p.Category).Select(g => new
            {
                Category = g.Key, ImageUrl = g.Select(p => p.ImageUrl).FirstOrDefault()
            }).ToDictionaryAsync(x => x.Category, x => x.ImageUrl);
        return imageUrl!;
    }

    public async Task<Dictionary<string, int>> GetTotalProductsByCategoryAsync(List<string> categories)
    {
        var result = await _context.Products
            .Where(p => !string.IsNullOrEmpty(p.Category) && categories.Contains(p.Category))
            .GroupBy(p => p.Category)
            .Select(g => new { Category = g.Key, ToralProductsByCategory = g.Count() })
            .ToDictionaryAsync(x => x.Category, x => x.ToralProductsByCategory);

        return result;
    }
}