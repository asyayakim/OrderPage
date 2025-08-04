namespace ECommerceApp.ApplicationLayer.DTO;

public class StoreDto
{
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
}

public class NutritionDto
{
        public string DisplayName { get; set; }
        public decimal? Amount { get; set; }
        public string Unit { get; set; }
}

public class ProductToSendDto
{
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string? Brand { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public string? Ingredients { get; set; }
        public StoreDto Store { get; set; }
        public List<NutritionDto> Nutrition { get; set; } = new();
}
