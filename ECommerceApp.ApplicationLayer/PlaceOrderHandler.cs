using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.Domain;
using ECommerceApp.Domain.Discounts;

namespace ECommerceApp.ApplicationLayer;

public class PlaceOrderHandler : IPlaceOrderHandler
{
    private readonly IOrderRepository _repository;

    public PlaceOrderHandler(IOrderRepository repository)
    {
        _repository = repository;
    }
    public async Task<Guid> Handle(CreateProductOrderDto command)
    {
        var order = new ProductOrder(command.CustomerId, command.Age);

        foreach (var item in command.Items)
        {
            order.AddProductItem(item.ProductId, item.Category, item.ImageUrl,
                item.Quantity, item.UnitPrice, item.Description, item.ProductName);
        }
        var strategies = new List<IDiscountStrategy>
        {
            new FruitDiscount(),
            new MeatDiscount(),
            new TobaccoDiscount(),
            new AlcoholDiscount()
        };

        order.CalculatePrice(strategies);

        await _repository.AddAsync(order);
        return order.Id;
    }
}