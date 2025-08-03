using ECommerceApp.ApplicationLayer.DTO;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class ProductServer
{
    private readonly AppDbContext _context;

    public ProductServer(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductToSendDto>> GetProductsFromDb()
    {
       var products = await _context.Products.ToListAsync();
       var productsToSend = new List<ProductToSendDto>();
       foreach (var product in products)
       {
           var productFromList = new ProductToSendDto()
           {
               ProductId = product.ProductId,
               ProductName = product.ProductName,
               Description = product.Description,
               ImageUrl = product.ImageUrl,
               Ingredients = product.Ingredients,
               //Store = product.Store,
               Quantity = product.Quantity,
               UnitPrice = product.UnitPrice,
           };
           productsToSend.Add(productFromList);
       }
       return productsToSend;
    }
}