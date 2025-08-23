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
            .Take(5).ToListAsync())!;
    }

    public async Task<List<string>> GetAllCategoriesAsync()
    {
        return await _context.Products
            .Where(p => !string.IsNullOrEmpty(p.Category))
            .Select(p => p.Category)
            .Distinct()
            .ToListAsync();
    }
}