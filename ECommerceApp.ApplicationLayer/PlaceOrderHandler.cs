using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.Domain;

namespace ECommerceApp.ApplicationLayer;

public record PlaceOrderCommand(Guid CustomerId, List<OrderItemDto> Items);
public class PlaceOrderHandler
{
    private readonly IOrderRepository _repository;

    public PlaceOrderHandler(IOrderRepository repository)
    {
        _repository = repository;
    }
    public async Task<Guid> Handle(PlaceOrderCommand command)
    {
        var order = new ProductOrder(command.CustomerId);

        foreach (var item in command.Items)
        {
            order.AddProductItem(item.ProductId, item.Category, item.ImageUrl, item.Quantity, item.UnitPrice);
        }
        var strategies = new List<IDiscountStrategy>
        {
            new FruitDiscount(),
            new MeatDiscount(),
        };

        order.CalculatePrice(strategies);

        await _repository.AddAsync(order);
        return order.Id;
    }
}