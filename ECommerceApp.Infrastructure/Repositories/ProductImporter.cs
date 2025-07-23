using System.Net.Http.Json;
using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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

    public async Task<List<Products>>  ImportProducts(int pageNumber, int pageSize )
    {
        var response = await _httpClient.GetAsync($"https://kassal.app/api/v1/products?page={pageNumber}&per_page={pageSize}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<KassalApiResponse>(content);

        foreach (var apiProduct in apiResponse.Data)
        {
            var product = new Products
            {
                UnitPrice = apiProduct.CurrentPrice,
                ProductName = apiProduct.Name,
                ImageUrl = apiProduct.Image ?? "Unknown",
                Description = apiProduct.Description
                 ?? "No description available",
                Store = apiProduct.Store?.Name ?? "Unknown",
                Ingridients = apiProduct.Ingredients
                ?? "Unknown"
            };

            try
            {
                await CreateEmbedding(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"embedding failed: {ex.Message}");
            }
            _context.Products.Add(product);
        }

        await _context.SaveChangesAsync();
        return _context.Products.ToList();
    }

    private async Task CreateEmbedding(Products product)
    {
        var payload = new
        {
            name = product.ProductName,
            description = product.Description, 
            ingredients = product.Ingridients
        };

        var response = await _httpClient.PostAsJsonAsync("http://localhost:5050/save_product_with_embedding", payload);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to save embedding to Flask service");
    }
}