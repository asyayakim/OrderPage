using System.Net.Http.Json;
using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class ProductImporter
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public ProductImporter(AppDbContext context, IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _context = context;
        _configuration = configuration;
        var apiKey = _configuration["KassalApiKey:ApiKey"];
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    }

    public async Task<List<Product>>  ImportProducts(int pageNumber, int pageSize )
    {
        var response = await _httpClient.GetAsync($"https://kassal.app/api/v1/products?page={pageNumber}&per_page={pageSize}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<KassalApiResponse>(content);

        foreach (var apiProduct in apiResponse.Data)
        {
            var storeName = apiProduct.Store?.Name?.Trim() ?? "Unknown";
            var storeCode = apiProduct.Store?.Code;
            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.Name == storeName && (storeCode == null || s.Code == storeCode));
            if (store == null)
            {
                store = new Store
                {
                    Name = storeName,
                    Code = storeCode,
                    Logo = apiProduct.Store?.Logo,
                    Url = apiProduct.Store?.Url
                };
                _context.Stores.Add(store);
                await _context.SaveChangesAsync();
            }
            var product = await _context.Products
                .Include(p => p.Nutrition)
                .Include(p => p.Store)
                .FirstOrDefaultAsync(p =>
                    p.ProductName == apiProduct.Name &&
                    p.Brand == apiProduct.Brand &&
                    p.StoreId == store.StoreId);

            if (product == null)
            {
                product = new Product
                {
                    ProductName = apiProduct.Name,
                    Brand = apiProduct.Brand ?? "Unknown",
                    ImageUrl = apiProduct.Image ?? "Unknown",
                    Description = apiProduct.Description ?? "No description available",
                    Ingredients = apiProduct.Ingredients ?? "Unknown",
                    UnitPrice = apiProduct.CurrentPrice,
                    StoreId = store.StoreId,
                    Store = store,
                    ExternalId = apiProduct.Id
                };
                _context.Products.Add(product);
            }
            // if (product.Nutrition.Any())
            // {
            //     _context.Nutritions.RemoveRange(product.Nutrition);
            // }
            if (apiProduct.Nutrition != null)
            {
                foreach (var nut in apiProduct.Nutrition)
                {
                    var name = string.IsNullOrWhiteSpace(nut.DisplayName) ? "Unknown" : nut.DisplayName.Trim();
                    product.Nutrition.Add(new Nutrition
                    {
                        DisplayName = name,
                        Amount = nut.Amount ?? null,
                        Unit = nut.Unit ?? null,
                        ProductId = product.ProductId
                    });
                }
                await _context.SaveChangesAsync();
            }
            try
            {
                await CreateEmbedding(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"embedding failed: {ex.Message}");
            }
        }

        await _context.SaveChangesAsync();
        return await _context.Products
            .Include(p => p.Store)
            .Include(p => p.Nutrition)
            .ToListAsync();
    }

    private async Task CreateEmbedding(Product product)
    {
        var nutritionList = product.Nutrition?
            .Select(n => new
            {
                displayName = n.DisplayName,
                amount = n.Amount,
                unit = n.Unit
            })
            .ToList();
        var payload = new
        {
            name = product.ProductName,
            brand = product.Brand,
            store = product.Store?.Name,
            unit_price = product.UnitPrice,
            external_id = product.ExternalId,
            description = product.Description,
            ingredients = product.Ingredients,
            nutrition = nutritionList
        };

        var response = await _httpClient.PostAsJsonAsync("http://localhost:5050/save_product_with_embedding", payload);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to save embedding to Flask service");
    }
}