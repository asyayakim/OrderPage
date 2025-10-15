using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public class Basket 
{
    [Key]
    public Guid BasketId { get; set; } = Guid.NewGuid();
    [Required]
    public Guid CustomerId  { get; set; }
    
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }

    public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    private Basket() { }

    public static Basket Create(Guid customerId)
    {
        //logic for quantity
        return new Basket
        {
            CustomerId = customerId,
        };
    }
}