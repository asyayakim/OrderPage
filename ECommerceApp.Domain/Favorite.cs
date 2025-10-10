using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public class Favorite : ProductBase
{
    [Required]
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public UserData User { get; set; }
}