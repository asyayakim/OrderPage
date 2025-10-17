using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public class Favorite : ProductBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required] 
    public Guid CustomerId { get; set; }
    [ForeignKey(nameof(CustomerId))] 
    public Customer Customer { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    
    private Favorite() { }
    public static Favorite Create(Guid customerId, Guid productId, Guid storeId)
    {
        return new Favorite
        {
            CustomerId = customerId,
            ProductId = productId,
            StoreId = storeId
        };
    }
}