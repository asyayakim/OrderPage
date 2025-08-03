using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain;

public class Product
{
    [Key]
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public int ExternalId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    [Required]
    public string ProductName { get; set; }
    public string? Brand { get; set; }
    public string? ImageUrl { get; set; }
    public string Description { get; set; }
    
    public string? Ingredients { get; set; }
    
    public Guid StoreId { get; set; }
    public Store Store { get; set; }
    
    public ICollection<Nutrition> Nutrition { get; set; } = new List<Nutrition>();
}

