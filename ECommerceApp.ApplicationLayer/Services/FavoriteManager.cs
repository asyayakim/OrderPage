using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.Domain;
using ECommerceApp.Domain.Interfaces;

namespace ECommerceApp.ApplicationLayer.Services;

public class FavoriteManager : IFavoriteManager
{
    private readonly IUserDataFavBasket _userDataFavBasket;

    public FavoriteManager(IUserDataFavBasket userDataFavBasket)
    {
        _userDataFavBasket = userDataFavBasket;
    }

    public async Task<List<object?>> GetAllFavorites()
    {
       var favorites = await _userDataFavBasket.GetAllFavoritesFromDb();
       return [favorites];
    }
    
}