namespace ECommerceApp.Domain;

public class Address
{
    public Address(string street, string city, string zipCode)
    {
        Street = street;
        City = "Oslo";
        ZipCode = zipCode;
        Country = "Norway";
    }

    public string Country { get; private set; }
    public string Street { get; }
    public string City { get; }
    public string ZipCode { get; }
}