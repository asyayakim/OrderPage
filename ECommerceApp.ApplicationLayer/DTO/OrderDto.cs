namespace ECommerceApp.ApplicationLayer.DTO
{

    public class OrderDto
    {
        public Guid? CustomerId { get; set; }
        public List<OrderItemDto>? Items { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public required string Category { get; set; }
        public string? ImageUrl { get; set; }
        public int? Discount { get; set; }
        public decimal? PriceWithDiscount { get; set; }
        public decimal PriceWithoutDiscount { get; set; }
        public string? Description { get; set; }
        public required string ProductName { get; set; }
    }
}