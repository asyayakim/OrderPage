namespace ECommerceApp.Domain;

public class FruitDiscount : IDiscountStrategy
{
    public int DiscountPercentage => 15;

    public bool IsApplicable(OrderItem item, List<OrderItem> allItems) =>
        item.Category.Equals("fruit", StringComparison.OrdinalIgnoreCase);

    public decimal ApplyDiscount(OrderItem item) =>
        item.Price * (1 - DiscountPercentage / 100m);
}