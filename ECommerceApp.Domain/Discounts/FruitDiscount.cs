namespace ECommerceApp.Domain.Discounts;

public class FruitDiscount : IDiscountStrategy
{
    public int DiscountPercentage => 15;

    public bool IsApplicable(OrderItem item, List<OrderItem> allItems,int age) =>
        item.Category.Equals("fruit", StringComparison.OrdinalIgnoreCase);

    public decimal ApplyDiscount(OrderItem item) =>
        item.Price * (1 - DiscountPercentage / 100m);
}