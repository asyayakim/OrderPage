namespace ECommerceApp.Domain.Interfaces;

public interface IUserDataFavBasket
{
    Task<List<Favorite>> GetAllFavoritesFromDb();
}