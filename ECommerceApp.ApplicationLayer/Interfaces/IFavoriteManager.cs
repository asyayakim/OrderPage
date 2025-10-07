namespace ECommerceApp.ApplicationLayer.Interfaces;

public interface IFavoriteManager
{
    Task<List<object?>> GetAllFavorites();
}