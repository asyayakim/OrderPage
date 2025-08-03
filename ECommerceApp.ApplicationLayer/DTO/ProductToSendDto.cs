namespace ECommerceApp.ApplicationLayer.DTO;

public class ProductToSendDto
{
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Store { get; set; }
        public string Ingridients { get; set; }
    
}