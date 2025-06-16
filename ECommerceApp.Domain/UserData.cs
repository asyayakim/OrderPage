using Microsoft.AspNetCore.Identity;

namespace ECommerceApp.Domain;

public class UserData : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int Age { get; set; }
}