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
}