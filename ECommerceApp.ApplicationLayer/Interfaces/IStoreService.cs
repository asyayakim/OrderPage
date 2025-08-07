namespace ECommerceApp.ApplicationLayer.Interfaces;

public interface IStoreService
{
    Task<List<string>> GetAllStoreNamesAsync();
}
