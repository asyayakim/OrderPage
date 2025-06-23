namespace ECommerceApp.Domain.Discounts;

public class AlcoholDiscount : IDiscountStrategy
{
    public bool IsApplicable(OrderItem item, List<OrderItem> allItems,int age)
        => item.Category.Equals("alcohol", 
                StringComparison.OrdinalIgnoreCase) 
           && age >= 21
           && allItems.Where(i => i.Category == "alcohol")
               .Sum(i => i.Quantity) >= 2; 

    public decimal ApplyDiscount(OrderItem item) =>
        item.Price * (1 - DiscountPercentage / 100m);

    public int DiscountPercentage { get; } = 17;
}