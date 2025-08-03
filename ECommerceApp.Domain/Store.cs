using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceApp.Domain;

public class Store
{
    [Key]
    public Guid StoreId { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; }
    public string Code { get; set; }    
    public string Logo { get; set; }
    public string Url { get; set; }
    [JsonIgnore]
    public ICollection<Product> Products { get; set; } = new List<Product>();
}