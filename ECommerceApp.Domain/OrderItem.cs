using System.Text.Json.Serialization;

namespace ECommerceApp.Domain;

public class OrderItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ProductId { get; private set; }
    public string Category { get; private set; }
    public string ImageUrl { get; private set; } 
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    [JsonIgnore]
    public decimal? PriceWithDiscount { get; private set; }
    [JsonIgnore]
    public int? Discount {get; private set;}
    public string? Description { get; private set; }
    public string ProductName { get; private set; }
    // [JsonIgnore]
    public ProductOrder ProductOrder { get; private set; } = null!;

    public OrderItem(Guid productId,string category, string imageUrl, int quantity, 
        decimal price, string ? description, string productName)
    {
        ProductId = productId;
        Category = category;
        ImageUrl = imageUrl;
        Quantity = quantity;
        Price = price;
        Description = description;
        ProductName =  productName;
    }
    public void SetUpdatedPrice(decimal newPrice)
    {
        PriceWithDiscount = newPrice;
    }

    public void SetDiscount(int percentage)
    {
        Discount = percentage;
    }
}