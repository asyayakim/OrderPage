namespace ECommerceApp.ApplicationLayer.Interfaces;

public interface IFavoriteManager
{
    Task<List<object?>> GetAllFavorites();
    Task<object?> AddFavorite(string userId, Guid productId);
}