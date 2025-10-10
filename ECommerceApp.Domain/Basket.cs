using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public class Basket : ProductBase
{
    [Required]
    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public UserData User { get; set; }
}