using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public abstract class ProductBase
{
    [Required]
    public Guid ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
    public Guid StoreId { get; set; }
    [ForeignKey(nameof(StoreId))]
    public Store Store { get; set; }
}