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

    public async Task<List<ProductToSendDto>> GetProductsFromDb(int pageNumber, int pageSize)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Store)
            .Include(p => p.Nutrition)
            .OrderBy(p => p.ProductName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductToSendDto
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice,
                ProductName = p.ProductName,
                Category = p.Category,
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
            })
            .ToListAsync();
    }

    public async Task<ProductToSendDto?> GetProductById(Guid id)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Store)
            .Include(p => p.Nutrition)
            .Where(p => p.ProductId == id)
            .Select(p => new ProductToSendDto
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice,
                ProductName = p.ProductName,
                Brand = p.Brand,
                Category = p.Category,
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
            })
            .FirstOrDefaultAsync();
    }

}
