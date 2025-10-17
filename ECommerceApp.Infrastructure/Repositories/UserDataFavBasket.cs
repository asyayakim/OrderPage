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
        
        await _context.Favorites.AddAsync(favorite);
        await _context.SaveChangesAsync();
        return favorite;
    }

    public async Task<object?> DeleteFavorite(string userId, Guid productId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);
        if (customer == null)
            return null;
        var product = await _context.Favorites.FirstOrDefaultAsync(p => p.ProductId == productId
                                                                        && p.CustomerId == customer.Id);

        _context.Favorites.Remove(product!);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<List<Basket>> GetAllProductsFromBasketsAllCustomersFromDb()
    {
        return await _context.Baskets.Include(b => b.Items).ToListAsync();
    }


    public async Task<IEnumerable<Favorite>> GetAllFavoritesByUserFromDb(Guid customerId)
    {
        return await _context.Favorites
            .Where(f => f.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<Basket?> AddProductToTheBasketToDb(Guid customerId, Basket basket)
    {
        var userBasket = await GetUserBasket(customerId);

        foreach (var item in basket.Items)
        {
            var newBasketItem = BasketItem.Create(
                userBasket.BasketId,
                item.ProductId,
                item.Quantity,
                item.StoreId
                );
            userBasket.AddItem(newBasketItem);
        }
        
        await _context.SaveChangesAsync();
        return userBasket;
    }

    public async Task<object?> DeleteFromBasket(Guid customerId, Guid productId)
    {
        var basket = await GetUserBasket(customerId);
        var product = await _context.BasketItems.FirstOrDefaultAsync(
            p => p.ProductId == productId);
        if (product == null)
        {
            return null;
        }
        if (product.Quantity >= 1)
            product.Quantity--;
        if (product.Quantity == 0)
            _context.Remove(product);
        await _context.SaveChangesAsync();
        return basket;
    }


    private async Task<Basket> GetUserBasket(Guid customerId)
    {
        var userBasket = await _context.
            Baskets.Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.CustomerId == customerId);
        if (userBasket == null)
        {
            userBasket = Basket.Create(customerId);
            _context.Baskets.Add(userBasket);
        }

        return userBasket;
    }
}