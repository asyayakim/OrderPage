using ECommerceApp.ApplicationLayer.DTO;

namespace ECommerceApp.ApplicationLayer.Interfaces;

public interface IStoreService
{
    Task<List<string>> GetAllStoreNamesAsync();
    Task<List<ProductToSendDto?>> GetAllProductsByStoreAsync(Guid storeid);
}
