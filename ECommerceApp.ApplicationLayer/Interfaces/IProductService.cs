using ECommerceApp.ApplicationLayer.DTO;

namespace ECommerceApp.ApplicationLayer.Interfaces;

public interface IProductService
{
    Task<List<ProductToSendDto>> GetProductsFromDb(int pageNumber, int pageSize);
    Task<ProductToSendDto?> GetProductById(Guid id);
    Task<ProductToSendDto?> GetOneAsync();
}