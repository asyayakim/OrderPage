using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.Domain.Interfaces;

namespace ECommerceApp.ApplicationLayer.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductToSendDto>> GetProductsFromDb(int pageNumber, int pageSize)
    {
        var products = await _productRepository.GetProductsFromDb(pageNumber, pageSize);
        return products.Select(p => new ProductToSendDto
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Brand = p.Brand,
            Category = p.Category,
            UnitPrice = p.UnitPrice,
            Quantity = p.Quantity,
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
                Unit = n.Unit,
                Amount = n.Amount
            }).ToList()
        }).ToList();
    }

    public async Task<ProductToSendDto?> GetProductById(Guid id)
    {
        var product = await _productRepository.GetProductById(id);
        if (product == null) return null;
        return new ProductToSendDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Brand = product.Brand,
            Category = product.Category,
            UnitPrice = product.UnitPrice,
            Quantity = product.Quantity,
            ImageUrl = product.ImageUrl,
            Description = product.Description,
            Ingredients = product.Ingredients,
            Store = new StoreDto
            {
                StoreId = product.Store.StoreId,
                Name = product.Store.Name,
                Code = product.Store.Code,
                Logo = product.Store.Logo,
                Url = product.Store.Url
            },
            Nutrition = product.Nutrition.Select(n => new NutritionDto
            {
                DisplayName = n.DisplayName,
                Unit = n.Unit,
                Amount = n.Amount
            }).ToList()
        };
    }

    public async Task<ProductToSendDto?> GetOneAsync()
    {
        var product = await _productRepository.GetOneAsync();
        if (product == null) return null;
        return new ProductToSendDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Brand = product.Brand,
            Category = product.Category,
            UnitPrice = product.UnitPrice,
            Quantity = product.Quantity,
            ImageUrl = product.ImageUrl,
            Description = product.Description,
            Ingredients = product.Ingredients,
            Store = new StoreDto
            {
                StoreId = product.Store.StoreId,
                Name = product.Store.Name,
                Code = product.Store.Code,
                Logo = product.Store.Logo,
                Url = product.Store.Url
            },
            Nutrition = product.Nutrition.Select(n => new NutritionDto
            {
                DisplayName = n.DisplayName,
                Unit = n.Unit,
                Amount = n.Amount
            }).ToList()
        };
    }
}