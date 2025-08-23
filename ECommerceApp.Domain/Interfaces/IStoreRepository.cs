namespace ECommerceApp.Domain.Interfaces;

public interface IStoreRepository
{
    Task<List<Store>> GetAllAsync();
    Task <List<Product?>> GetAllProductsByStoreIdAsync(Guid storeId);
    Task<List<Product?>> GetAllTopSellersAsync();
    Task<List<string>> GetAllCategoriesAsync();
}