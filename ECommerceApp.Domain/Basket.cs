using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public class Basket : ProductBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public Guid CustomerId  { get; set; }
    
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }

    public int Quantity { get; set; }
}