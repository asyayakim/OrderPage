using Microsoft.EntityFrameworkCore; 
using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.Domain;
using Order.Infrastructure.Persistence;


namespace Order.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ProductOrder productOrder)
    {
        await _context.Orders.AddAsync(productOrder);
        await _context.SaveChangesAsync();
    }
    public async Task<List<OrderDto>> GetByOrderAsync(Guid id)
    {
        var orders = await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.CustomerId == id)
            .ToListAsync();

        return orders.Select(order => new OrderDto
        {
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPriceWithDiscount,
            TotalPriceWithoutDiscount = order.TotalPriceWithoutDiscount,
            Items = order.Items.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                Category = item.Category,
                ImageUrl = item.ImageUrl,
                Discount = item.Discount,
                Description = item.Description,
                ProductName = item.ProductName,
                PriceWithDiscount = item.PriceWithDiscount,
            }).ToList()
        }).ToList();
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
        var products = await _context.Orders.Include(o => o.Items).ToListAsync();
        return products.Select(product => new OrderDto
        {
            CustomerId = product.CustomerId,
            OrderDate = product.OrderDate,
            TotalPrice = product.TotalPriceWithDiscount,
            
            Items = product.Items.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                Category = item.Category,
                ImageUrl = item.ImageUrl,
                Discount = item.Discount,
                PriceWithDiscount = item.PriceWithDiscount,
                ProductName = item.ProductName
            }).ToList()
        }).ToList();
    }

    public async Task<ProductOrder?> RemoveOrderAsync(Guid id)
    {
       var order = await _context.Orders.FindAsync(id);
       _context.Orders.Remove(order?? throw new Exception("Order not found"));
       await _context.SaveChangesAsync();
       return order;
    }
}