namespace ECommerceApp.Domain.Interfaces;

public interface IStoreRepository
{
    Task<List<Store>> GetAllAsync();
    Task <List<Product?>> GetAllProductsByStoreIdAsync(Guid storeId);
    Task<List<Product?>> GetAllTopSellersAsync();
    Task<List<string>> GetAllCategoriesAsync();
    Task<List<Product>> GetAllByCategoryAsync(string category, int pageNumber, int pageSize);
}