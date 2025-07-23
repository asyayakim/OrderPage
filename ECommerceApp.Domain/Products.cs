using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain;

public class Products
{
    [Key]
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public string Store { get; set; }
    public string Ingridients { get; set; }
}