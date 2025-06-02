using ECommerceApp.Domain;

namespace ApplicationLayer;

public record PlaceOrderCommand(Guid CustomerId, List<OrderItemDto> Items);
public record OrderItemDto(Guid ProductId, int Quantity, decimal UnitPrice);

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
            order.AddProductItem(item.ProductId, item.Quantity, item.UnitPrice);
        }

        await _repository.AddAsync(order);
        return order.Id;
    }
}