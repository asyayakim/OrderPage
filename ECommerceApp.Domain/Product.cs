namespace ECommerceApp.Domain;

public class Product
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; private set; } = new();
    private Product() {}
    public DateTime CreatedAt { get; private set; }
    public Product(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
    }
    public void AddProductItem(Guid productId, int quantity, decimal unitPrice)
    {
        Items.Add(new OrderItem
            (productId, quantity, unitPrice));
    }
}