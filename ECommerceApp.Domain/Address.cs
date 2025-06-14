namespace ECommerceApp.Domain;

public class Address
{
    private Address() { }
    public Address(string street, string zipCode)
    {
        Street = street;
        City = "Oslo";
        ZipCode = zipCode;
        Country = "Norway";
    }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Street { get; private set; }
    public string City { get; private set; } = "Oslo";
    public string ZipCode { get; private set; }
    public string Country { get; private set; } = "Norway";
    
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }
}