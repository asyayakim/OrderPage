namespace ECommerceApp.ApplicationLayer.DTO;

public class UserDto
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public DateOnly Birthday { get; set; }
    public Guid UserId { get; set; }
}