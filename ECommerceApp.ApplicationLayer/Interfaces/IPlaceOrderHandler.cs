using ECommerceApp.ApplicationLayer.DTO;

namespace ECommerceApp.ApplicationLayer.Interfaces;

public interface IPlaceOrderHandler
{
    public Task<Guid> Handle(CreateProductOrderDto command);
}