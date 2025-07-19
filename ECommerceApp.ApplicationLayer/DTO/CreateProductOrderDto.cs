namespace ECommerceApp.ApplicationLayer.DTO;

public class CreateProductOrderDto
{
    public List<CreateOrderItemDto> Items { get; set; }
    public Guid CustomerId {get; set;}
    public int Age {get; set;}
    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; } 
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}


