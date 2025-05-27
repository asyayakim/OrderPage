using System.Data.Entity;
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

    public async Task AddAsync(ECommerceApp.Domain.Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<ECommerceApp.Domain.Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include("._items") 
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}