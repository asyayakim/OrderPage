namespace ECommerceApp.Domain;

public class ProductOrder
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    
    //public double TotalPrice { get; private set; }
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


    public void CalculatePrice(ProductOrder productOrder)
    {
        if (!Items.Any())
            return;
        var category = productOrder.Items.Select(item => item.Category).Distinct().Single();
        var amount = productOrder.Items.Sum(item => item.Quantity);
        foreach (var orderItem in productOrder.Items)
        {
            var price = orderItem.Price;
            if (category == "fruit")
            {
                price *= 0.95m;
            }

            if (category == "meat" && amount >= 3)
            {
                price *= 0.70m;
            }
            orderItem.SetUpdatedPrice(price);
        }
       
    }
}