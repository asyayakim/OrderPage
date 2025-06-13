namespace ECommerceApp.Domain;

public class ProductOrder
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    
    public decimal TotalPriceWithDiscount { get; private set; }
    public decimal TotalPriceWithoutDiscount { get; private set; }
    public ProductOrder(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
    }
    public void AddProductItem(Guid productId, string category, string imageUrl,
        int quantity, decimal unitPrice, string description, string productName)
    {
        Items.Add(new OrderItem
            (productId, category, imageUrl, quantity, unitPrice, description, productName));
    }

    public void CalculatePrice(List<IDiscountStrategy> strategies)
    {
        if (!Items.Any()) return;

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