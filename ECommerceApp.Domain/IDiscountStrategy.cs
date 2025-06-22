namespace ECommerceApp.Domain;

public interface IDiscountStrategy
{
    bool IsApplicable(OrderItem item, List<OrderItem> allItems, int age);
    decimal ApplyDiscount(OrderItem item);
    int DiscountPercentage { get; }
}