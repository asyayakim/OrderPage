using ECommerceApp.Domain;
using ECommerceApp.Domain.Discounts;
using Xunit.Sdk;

namespace ECommerce.Tests;

public class ProductOrderTests
{
    [Fact]
    public void CheckApplyDiscountFruit()
    {
        var order = new ProductOrder(Guid.NewGuid(), 19);
        order.AddProductItem(Guid.NewGuid(), "fruit", "picture.png", 5,
            25, "something", "name");
      
        var strategies = new List<IDiscountStrategy>
        {
            new FruitDiscount(),
        };
        order.CalculatePrice(strategies);

        var item = order.Items.First();
        Assert.Equal(15, item.Discount);
        Assert.Equal(21.25m, item.UnitPriceWithDiscount);
        Assert.Equal(125m, order.TotalPriceWithoutDiscount);
        Assert.Equal(25m, item.Price);
        Assert.Equal(106.25m, order.TotalPriceWithDiscount);
    }
    [Fact]
    public void CheckApplyDiscountMeat()
    {
        var order = new ProductOrder(Guid.NewGuid(), 19);
        order.AddProductItem(Guid.NewGuid(), "meat", "picture.png", 5,
            25, "something", "name");
      
        var strategies = new List<IDiscountStrategy>
        {
            new MeatDiscount(),
        };
        order.CalculatePrice(strategies);

        var item = order.Items.First();
        Assert.Equal(30, item.Discount);
        Assert.Equal(17.5m, item.UnitPriceWithDiscount);
        Assert.Equal(125m, order.TotalPriceWithoutDiscount);
    }
    [Fact]
    public void OnlyMatchingItems_GetDiscount()
    {
        var order = new ProductOrder(Guid.NewGuid(), userAge: 19);
        order.AddProductItem(Guid.NewGuid(), "fruit", "fruit.png", 2, 10, "something",
            "name"); 
        order.AddProductItem(Guid.NewGuid(), "meat", "meat.png", 1, 50, "something",
            "name");  

        var strategies = new List<IDiscountStrategy>
        {
            new FruitDiscount(),
            new MeatDiscount()
        };

        order.CalculatePrice(strategies);

        var fruit = order.Items.First(i => i.Category == "fruit");
        var meat = order.Items.First(i => i.Category == "meat");

        Assert.Equal(15, fruit.Discount);
        Assert.Equal(8.5m, fruit.UnitPriceWithDiscount);

        Assert.Null(meat.Discount);
        Assert.Equal(50, meat.UnitPriceWithDiscount);

        var expectedTotal = (8.5m * 2) + (50 * 1);
        Assert.Equal(expectedTotal, order.TotalPriceWithDiscount);
    }

    [Fact]
    public void TestTobaccoPurchase_Under18_ThrowsException()
    {
        var order = new ProductOrder(Guid.NewGuid(), userAge: 17);

        var exeption = Assert.Throws<InvalidOperationException>(() =>
        {
            order.AddProductItem(Guid.NewGuid(), "tobacco", "tobacco.png", 1, 90, "something", "Malbrr");
        });
        Assert.Equal("Customers under 18 cannot purchase tobacco products.", exeption.Message);
    }

}