namespace ECommerceApp.ApplicationLayer.DTO
{

    public class OrderDto
    {
        public Guid? CustomerId { get; set; }
        public List<OrderItemDto>? Items { get; set; }
        public DateTime? OrderDate { get; set; }
    }

    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
    }
}