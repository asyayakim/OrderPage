using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.Domain.Interfaces;

namespace ECommerceApp.ApplicationLayer.Services;
public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;

    public StoreService(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }


    public async Task<List<string>> GetAllStoreNamesAsync()
    {
        var stores = await _storeRepository.GetAllAsync();
        return stores.Select(s => s.Name).ToList();
    }
}