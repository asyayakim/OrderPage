using ECommerceApp.Domain;
namespace ECommerceApp.ApplicationLayer.DTO;

public class CreateUserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public int Age { get; set; }

    public UserData ToUserData()
    {
        if (Age < 14)
        {
            throw new Exception("Registration is not allowed for age under 14");
        }
        return new UserData
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            UserName = Email,
            Age = Age
        };
    }
}
