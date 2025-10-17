namespace ECommerceApp.Domain.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProductsFromDb(int pageNumber, int pageSize);
    Task<Product?> GetProductById(Guid id);
    Task<Product?> GetOneAsync();
}