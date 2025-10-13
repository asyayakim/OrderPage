namespace ECommerceApp.Domain.Interfaces;

public interface IUserDataFavBasket
{
    Task<List<Favorite>> GetAllFavoritesFromDb();
    Task<object?> AddFavorite(string userId, Guid productId);
}