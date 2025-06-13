using ECommerceApp.Domain;

namespace Order.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public Task<Customer?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<object?> GetAllCustomers()
    {
        throw new NotImplementedException();
    }
}