namespace ECommerceApp.Domain.Interfaces;

public interface IStoreRepository
{
    Task<List<Store>> GetAllAsync();
    Task <List<Product?>> GetAllProductsByStoreIdAsync(Guid storeId);
}