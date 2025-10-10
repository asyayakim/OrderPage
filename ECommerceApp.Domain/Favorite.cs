using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public class Favorite : ProductBase
{
    [Required] 
    public Guid CustomerId { get; set; }
    [ForeignKey(nameof(CustomerId))] 
    public Customer Customer { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}