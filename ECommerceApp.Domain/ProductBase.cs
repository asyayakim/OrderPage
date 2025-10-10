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

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
}