namespace ECommerceApp.Domain;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
    private List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    private Order() {}
    public DateTime CreatedAt { get; private set; }
    public Order(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
    }
    public void AddItem(Guid productId, int quantity, decimal unitPrice)
    {
        _items.Add(new OrderItem
            (productId, quantity, unitPrice));
    }
}