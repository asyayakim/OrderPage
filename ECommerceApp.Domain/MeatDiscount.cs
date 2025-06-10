namespace ECommerceApp.Domain;

public class MeatDiscount : IDiscountStrategy
{
    public bool IsApplicable(OrderItem item, List<OrderItem> allItems)
    => item.Category.Equals("meat", 
        StringComparison.OrdinalIgnoreCase)
    && allItems.Where(i => i.Category == "meat").
    Sum(i => i.Price * i.Quantity) >= 3;

    public decimal ApplyDiscount(OrderItem item) =>
        item.Price * (1 - DiscountPercentage / 100m);

    public int DiscountPercentage => 30;
}