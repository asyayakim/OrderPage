using ECommerceApp.ApplicationLayer.DTO;

namespace ECommerceApp.ApplicationLayer.Interfaces;

public interface IStoreService
{
    Task<List<StoreDto>> GetAllStoreNamesAsync();
    Task<List<ProductToSendDto?>> GetAllProductsByStoreAsync(Guid storeid);
    Task<object?> GetTopSellersAsync();
    Task<object?> GetCategoriesAsync();
    Task<object?> GetProductsByCategory(string? category, int pageNumber, int pageSize, decimal? minPrice, decimal? maxPrice, string? sortBy);
    Task<object?> GetFeaturedProducts(int limit);
}
