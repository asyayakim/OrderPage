namespace ECommerceApp.Domain;

public class ProductOrder
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    public ProductOrder(Guid customerId)
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