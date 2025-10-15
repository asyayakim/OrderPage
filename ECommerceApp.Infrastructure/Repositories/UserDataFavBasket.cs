using ECommerceApp.Domain;
using ECommerceApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class UserDataFavBasket : IUserDataFavBasket
{
    private readonly AppDbContext _context;

    public UserDataFavBasket(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Favorite>> GetAllFavoritesFromDb()
    {
        return await _context.Favorites.ToListAsync();
    }

    public Task<object?> AddFavorite(string userId, Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task<Favorite?> AddFavorite(Guid customerId, Guid productId, Guid storeId)
    {
        var exists = await _context.Favorites
            .FirstOrDefaultAsync(f => f.ProductId == productId && f.CustomerId == customerId);
        if (exists != null)
            return null;
        var favorite = Favorite.Create(customerId, productId, storeId);
        var productExists = await _context.Products.AnyAsync(p => p.ProductId == productId);
        if (!productExists)
            throw new InvalidOperationException("Cannot add favorite: Product not found in database.");
        

        Console.WriteLine($"Product {productId} exists in DB? {productExists}");

        await _context.Favorites.AddAsync(favorite);
        await _context.SaveChangesAsync();
        return favorite;
    }

    public async Task<object?> DeleteFavorite(string userId, Guid productId)
    {
        var customer = await _context.Customers.
            FirstOrDefaultAsync(u => u.UserId.ToString() == userId);
        if (customer == null)
            return null;
        var product = await _context.Favorites.
            FirstOrDefaultAsync(p =>p.ProductId == productId 
                                    && p.CustomerId == customer.Id);
        
        _context.Favorites.Remove(product!);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<IEnumerable<Favorite>> GetByCustomerIdAsync(Guid customerId)
    {
        return await _context.Favorites
            .Where(f => f.CustomerId == customerId)
            .ToListAsync();
    }
}