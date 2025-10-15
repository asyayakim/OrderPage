using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.ApplicationLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.Domain;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _dbContext;

    public CustomerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer> GetByIdAsync(Guid id)
    {
        var user = await _dbContext.Customers
            .Include(c => c.Address)
            .FirstOrDefaultAsync(c => c.UserId == id);
        if (user == null)
        {
            throw new ArgumentException("Customer not found");
        }

        return user;
    }

    public async Task AddAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        await _dbContext.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        var allClients = await _dbContext
            .Customers.Include(a => a.Address).ToListAsync();
        if (allClients == null)
        {
            throw new ArgumentException("No Customers were found");
        }

        return allClients;
    }

    public async Task<object> AddDataToUser(UserData user, CustomerDto dto)
    {
        var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.UserId == user.Id);
        if (customer == null)
        {
            throw new ArgumentException("Customer not found");
        }
        var address = await _dbContext.Addresses
            .FirstOrDefaultAsync(a => a.CustomerId == customer.Id);
        if (address == null)
        {
            address = new Address(dto.Street, dto.ZipCode, customer.Id, user.Id);
            _dbContext.Addresses.Add(address);
        }
        else
        { 
            address.Update(dto.Street, dto.ZipCode);
        }
        await _dbContext.SaveChangesAsync();
        return address;
    }
}