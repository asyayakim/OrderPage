namespace ECommerceApp.Domain;

public class ProductOrder
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    
    public decimal TotalPriceWithDiscount { get; private set; }
    public decimal TotalPriceWithoutDiscount { get; private set; }
    private int UserAge { get; set; }
    public ProductOrder(Guid customerId, int userAge)
    {
        CustomerId = customerId;
        UserAge = userAge;
    }
    private ProductOrder() { }
    public void AddProductItem(Guid productId, string category, string imageUrl,
        int quantity, decimal unitPrice, string description, string productName)
    {
        Items.Add(new OrderItem
            (productId, category, imageUrl, quantity, unitPrice, description, productName));
    }

    public void CalculatePrice(List<IDiscountStrategy> strategies)
    {
        if (!Items.Any()) return;
        if (!Items.Any()) return;

        if (UserAge < 18 && Items.Any(i => i.Category.Equals("tobacco", StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Customers under 18 cannot purchase tobacco products.");
        }

        TotalPriceWithDiscount = 0;
        TotalPriceWithoutDiscount = 0;

        foreach (var item in Items)
        {
            var discount = strategies.FirstOrDefault(s => s.IsApplicable(item, Items));
            if (discount != null)
            {
                var newPrice = discount.ApplyDiscount(item);
                item.SetUpdatedPrice(newPrice);
                item.SetDiscount(discount.DiscountPercentage);
                TotalPriceWithDiscount += newPrice * item.Quantity;
                TotalPriceWithoutDiscount += item.Quantity * item.Price;
            }
            else
            {
                item.SetUpdatedPrice(item.Price);
                TotalPriceWithDiscount += item.Price * item.Quantity;
                TotalPriceWithoutDiscount += item.Quantity * item.Price;
            }
        }
    }
}