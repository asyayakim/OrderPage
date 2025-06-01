namespace ECommerceApp.Domain
{
    public interface IOrderRepository
    {
        Task AddAsync(Product product);
    }
}