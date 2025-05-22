namespace ECommerceApp.Domain;

public interface IOrderRepository
{
    Task AddAsync(ECommerceApp.Domain.Order order);
    Task<ECommerceApp.Domain.Order?> GetByIdAsync(Guid id);
}