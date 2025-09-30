using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace ECommerceApp.Domain;

public class UserData : IdentityUser<Guid>
{
    public UserData()
    {
        Id = Guid.NewGuid(); 
    }
    [MaxLength(100)] public string FirstName { get; set; }

    [MaxLength(100)] public string LastName { get; set; }
    public DateOnly Birthday{ get; set; } = new DateOnly();
}