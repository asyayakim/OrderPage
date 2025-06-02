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

    public async Task AddAsync(Product product)
    {
        await _context.Orders.AddAsync(product);
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
            Items = order.Items.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Price
            }).ToList()
        }).ToList();
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
        var products = await _context
            .Orders.Include(o => o.Items).
            ThenInclude(i => i.Product).ToListAsync();
        return products.Select(product => new OrderDto
        {
            CustomerId = product.CustomerId,
            OrderDate = product.OrderDate,
            Items = product.Items.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Price
            }).ToList()
        }).ToList();
    }
}