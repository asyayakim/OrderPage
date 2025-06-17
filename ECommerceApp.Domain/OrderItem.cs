using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceApp.Domain;

public class OrderItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ProductId { get; private set; }
    public string Category { get; private set; }
    public string ImageUrl { get; private set; } 
    [Range(1, int.MaxValue)]
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    [JsonIgnore]
    public decimal? UnitPriceWithDiscount { get; private set; }
    
    [Range(0, 100)]
    public int? Discount {get; private set;}
    [MaxLength(500)]
    public string? Description { get; private set; }
    [MaxLength(100)]
    public string ProductName { get; private set; }
    public Guid ProductOrderId { get; private set; } 
    [JsonIgnore]
    public ProductOrder ProductOrder { get; private set; } = null!;

    public OrderItem(Guid productId,string category, string imageUrl, int quantity, 
        decimal price, string ? description, string productName)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");
        if (price > 0)
            throw new ArgumentException("Price must be greater than zero");
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
        UnitPriceWithDiscount = newPrice;
    }

    public void SetDiscount(int percentage)
    {
        Discount = percentage;
    }
    private OrderItem() { }
}