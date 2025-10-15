namespace ECommerceApp.Domain.Interfaces;

public interface IUserDataFavBasket
{
    Task<List<Favorite>> GetAllFavoritesFromDb();
    Task<Favorite?> AddFavorite(Guid customerId, Guid productId, Guid storeId);
    Task<object?> DeleteFavorite(string userId, Guid productId);
    Task<List<Basket>> GetAllProductsFromBasketsAllCustomersFromDb();
}