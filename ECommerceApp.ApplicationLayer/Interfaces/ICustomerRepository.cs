using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.Domain;

namespace ECommerceApp.ApplicationLayer.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(Guid id);
    Task AddAsync(Customer customer);
    Task<List<Customer>> GetAllCustomers();
    Task<object> AddDataToUser(UserData customer, CustomerDto dto);
}
