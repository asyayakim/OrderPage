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
        var favorites = await _context.Favorites.ToListAsync();
        return favorites;
    }
}