using ECommerceApp.Domain;
using ECommerceApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class UserDataFavBasket : IUserDataFavBasket
{
    private readonly AppDbContext _context;
    private readonly ProductServer _productServer;

    public UserDataFavBasket(AppDbContext context, ProductServer productServer)
    {
        _context = context;
        _productServer = productServer;
    }

    public async Task<List<Favorite>> GetAllFavoritesFromDb()
    {
        return await _context.Favorites.ToListAsync();
    }

    public async Task<object?> AddFavorite(string userId, Guid productId)
    {
        var customer = await _context.Customers.
            FirstOrDefaultAsync(u => u.UserId.ToString() == userId);
        if (customer == null)
            return null;
        var product = await _productServer.GetProductById(productId);
        var addToFav = await _context.Favorites.AddAsync( 
            new Favorite
            {
                ProductId = productId,
                CustomerId = customer.Id,
                StoreId = product!.Store.StoreId,
            });
        await _context.SaveChangesAsync();
        return addToFav.Entity;
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
}