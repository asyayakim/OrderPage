using ECommerceApp.ApplicationLayer.DTO;

namespace ECommerceApp.ApplicationLayer.Interfaces;

public interface IStoreService
{
    Task<List<StoreDto>> GetAllStoreNamesAsync();
    Task<List<ProductToSendDto?>> GetAllProductsByStoreAsync(Guid storeid);
    Task<object?> GetTopSellersAsync();
    Task<object?> GetCategoriesAsync();
}
