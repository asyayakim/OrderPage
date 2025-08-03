using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain;

public class Nutrition
{
    [Key]
    public Guid NutritionId { get; set; } = Guid.NewGuid();

    public string DisplayName { get; set; }
    public decimal? Amount { get; set; }
    public string Unit { get; set; }
    
    public Guid? ProductId { get; set; }
    public Product Product { get; set; }
}