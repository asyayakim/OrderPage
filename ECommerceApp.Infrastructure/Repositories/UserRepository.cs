using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.Domain;
using Microsoft.AspNetCore.Identity;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class UserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<object> SaveToDbAsync(Customer customer)
    {
        await _dbContext.Customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
        return customer;
    }
}