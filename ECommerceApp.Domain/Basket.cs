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
        return new Basket
        {
            CustomerId = customerId,
        };
    }

    public void AddItem(BasketItem item)
    {
        var itemToAdd = Items.SingleOrDefault(i => i.ProductId == item.ProductId);
        if (itemToAdd != null)
        {
            itemToAdd.Quantity += item.Quantity;
        }
        else
        {
            Items.Add(item);
        }
    }
}