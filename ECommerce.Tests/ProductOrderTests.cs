using ECommerceApp.Domain;
using Xunit.Sdk;

namespace ECommerce.Tests;

public class ProductOrderTests
{
    [Fact]
    public void CheckApplyDiscountFruit()
    {
        var order = new ProductOrder(Guid.NewGuid());
        order.AddProductItem(Guid.NewGuid(), "fruit", "picture.png", 5,
            25, "something", "name");
      
        var strategies = new List<IDiscountStrategy>
        {
            new FruitDiscount(),
        };
        order.CalculatePrice(strategies);

        var item = order.Items.First();
        Assert.Equal(15, item.Discount);
    }
    [Fact]
    public void CheckApplyDiscountMeat()
    {
        var order = new ProductOrder(Guid.NewGuid());
        order.AddProductItem(Guid.NewGuid(), "meat", "picture.png", 5,
            25, "something", "name");
      
        var strategies = new List<IDiscountStrategy>
        {
            new MeatDiscount(),
        };
        order.CalculatePrice(strategies);

        var item = order.Items.First();
        Assert.Equal(30, item.Discount);
    }
}