namespace ECommerceApp.ApplicationLayer.DTO;

public class CategoryWithTotalProductsDto
{
    public string? Category { get; set; }
    public string? ImageUrl { get; set; }
    public int? ToralProductsByCategory { get; set; }
}