using ECommerceApp.Domain;
namespace ECommerceApp.ApplicationLayer.DTO;

public class CreateUserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public UserData ToUserData()
    {
        return new UserData
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            UserName = Email,
        };
    }
}
