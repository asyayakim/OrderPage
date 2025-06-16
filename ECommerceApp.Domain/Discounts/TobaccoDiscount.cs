namespace ECommerceApp.Domain;

public class TobaccoDiscount : IDiscountStrategy
{
    public bool IsApplicable(OrderItem item, List<OrderItem> allItems)
        => item.Category.Equals("tobacco", 
               StringComparison.OrdinalIgnoreCase)
           && allItems.Where(i => i.Category == "tobacco").
               Sum(i => i.Price * i.Quantity) >= 12;

    public decimal ApplyDiscount(OrderItem item) =>
        item.Price * (1 - DiscountPercentage / 100m);

    public int DiscountPercentage { get; } = 12;
}