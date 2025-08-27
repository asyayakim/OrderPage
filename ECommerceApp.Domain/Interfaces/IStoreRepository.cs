namespace ECommerceApp.Domain.Interfaces;

public interface IStoreRepository
{
    Task<List<Store>> GetAllAsync();
    Task <List<Product?>> GetAllProductsByStoreIdAsync(Guid storeId);
    Task<List<Product?>> GetAllTopSellersAsync();
    Task<List<string>> GetAllCategoriesAsync();
    Task<List<Product>> GetAllByCategoryAsync(string? category, int pageNumber, int pageSize,  decimal? minPrice, decimal? maxPrice, string? sortBy);
    Task<List<Product>> GetAllFeaturedAsync(int limit);
    Task<Dictionary<string, string>> GetImageUrlByCategoryName(List<string> categories);
    Task<Dictionary<string, int>> GetTotalProductsByCategoryAsync(List<string> categories);
}