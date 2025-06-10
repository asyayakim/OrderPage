namespace ECommerceApp.Domain;

public interface IDiscountStrategy
{
    bool IsApplicable(OrderItem item, List<OrderItem> allItems);
    decimal ApplyDiscount(OrderItem item);
    int DiscountPercentage { get; }
}