namespace ECommerceApp.Domain.Interfaces;

public interface IStoreRepository
{
    Task<List<Store>> GetAllAsync(); 
}