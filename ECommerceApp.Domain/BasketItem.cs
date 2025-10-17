using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public class BasketItem : ProductBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid BasketId { get; set; }

    [ForeignKey(nameof(BasketId))]
    public Basket Basket { get; set; }

    [Required]
    public int Quantity { get; set; }

    private BasketItem() { }

    public static BasketItem Create(Guid basketId, Guid productId, int quantity, Guid storeId)
    {
        return new BasketItem
        {
            BasketId = basketId,
            ProductId = productId,
            Quantity = quantity,
            StoreId = storeId
        };
    }
}