using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public abstract class ProductBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
    public Guid StoreId { get; set; }
    [ForeignKey(nameof(StoreId))]
    public Store Store { get; set; }
    public Guid NutritionId { get; set; }
    [ForeignKey(nameof(NutritionId))]
    public Nutrition Nutrition { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
}