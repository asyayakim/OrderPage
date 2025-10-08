namespace ECommerceApp.Domain.Interfaces;

public interface IUserDataFavBasket
{
    Task<List<object>> GetAllFavoritesFromDb();
}