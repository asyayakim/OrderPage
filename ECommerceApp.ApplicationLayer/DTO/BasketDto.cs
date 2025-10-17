using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.ApplicationLayer.DTO;

public class BasketDto
{
    public ICollection<BasketItemDto> Items { get; set; } 
}

public class BasketItemDto
{
    public Guid ProductId { get; set; }

    [Required] public int Quantity { get; set; } = 1;
    public Guid StoreId { get; set; }
    // public Guid BasketId { get; set; }
}