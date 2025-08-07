using ECommerceApp.ApplicationLayer.DTO;
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

    public async Task<List<ProductToSendDto?>> GetAllProductsByStoreAsync(Guid storeId)
    {
        var products = await _storeRepository.GetAllProductsByStoreIdAsync(storeId);
        var productToSendDto = new List<ProductToSendDto>();
        foreach (var p in products)
        {
            var dto = new ProductToSendDto
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice,
                ProductName = p.ProductName,
                Brand = p.Brand,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Ingredients = p.Ingredients,
                Store = new StoreDto
                {
                    StoreId = p.Store.StoreId,
                    Name = p.Store.Name,
                    Code = p.Store.Code,
                    Logo = p.Store.Logo,
                    Url = p.Store.Url
                },
                Nutrition = p.Nutrition.Select(n => new NutritionDto
                {
                    DisplayName = n.DisplayName,
                    Amount = n.Amount,
                    Unit = n.Unit
                }).ToList()
            };
            productToSendDto.Add(dto);
        }
        return productToSendDto;
    }
}