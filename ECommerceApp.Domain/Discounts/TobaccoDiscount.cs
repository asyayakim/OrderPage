namespace ECommerceApp.Domain.Discounts;

public class TobaccoDiscount : IDiscountStrategy
{
    public bool IsApplicable(OrderItem item, List<OrderItem> allItems,int age)
        => item.Category.Equals("tobacco", 
               StringComparison.OrdinalIgnoreCase) && age >= 18
           && allItems.Where(i => i.Category == "tobacco").
               Sum(i => i.Quantity) >= 12; 

    public decimal ApplyDiscount(OrderItem item) =>
        item.Price * (1 - DiscountPercentage / 100m);

    public int DiscountPercentage { get; } = 12;
}