namespace ECommerceApp.ApplicationLayer.DTO;

public class CustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public Guid UserId { get; set; }
    
    public DateOnly Birthday { get; set; }
}