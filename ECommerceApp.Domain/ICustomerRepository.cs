namespace ECommerceApp.Domain;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
    Task AddAsync(Customer customer);
    Task<object?> GetAllCustomers();
}
