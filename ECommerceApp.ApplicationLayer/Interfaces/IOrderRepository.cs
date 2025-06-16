using ECommerceApp.Domain;

namespace ECommerceApp.ApplicationLayer.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(ProductOrder productOrder);
    }
}