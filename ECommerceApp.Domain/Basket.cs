using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public class Basket : ProductBase
{
    [Required]
    public Guid CustomerId  { get; set; }
    
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }

    public int Quantity { get; set; }
}