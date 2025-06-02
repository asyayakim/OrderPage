namespace ECommerceApp.Domain
{
    public interface IOrderRepository
    {
        Task AddAsync(ProductOrder productOrder);
    }
}