namespace ECommerceApp.ApplicationLayer.Interfaces;

public interface IPlaceOrderHandler
{
    public Task<Guid> Handle(PlaceOrderCommand command);
}