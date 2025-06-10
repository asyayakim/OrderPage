namespace ECommerceApp.Domain;

public class ProductOrder
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    
    public decimal TotalPrice { get; private set; }
    public ProductOrder(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
    }
    public void AddProductItem(Guid productId, string category, string imageUrl,int quantity, decimal unitPrice)
    {
        Items.Add(new OrderItem
            (productId, category, imageUrl, quantity, unitPrice));
    }

    public void CalculatePrice(List<IDiscountStrategy> strategies)
    {
        if (!Items.Any()) return;

        TotalPrice = 0;

        foreach (var item in Items)
        {
            var discount = strategies.FirstOrDefault(s => s.IsApplicable(item, Items));
            if (discount != null)
            {
                var newPrice = discount.ApplyDiscount(item);
                item.SetUpdatedPrice(newPrice);
                item.SetDiscount(discount.DiscountPercentage);
                TotalPrice += newPrice * item.Quantity;
            }
            else
            {
                item.SetUpdatedPrice(item.Price);
                TotalPrice += item.Price * item.Quantity;
            }
        }
    }
}