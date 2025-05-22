namespace ECommerceApp.Domain;

public class Address
{
    public Address(string street, string city, string zipCode)
    {
        Street = street;
        City = city;
        ZipCode = zipCode;
    }

    public string Street { get; }
    public string City { get; }
    public string ZipCode { get; }
}