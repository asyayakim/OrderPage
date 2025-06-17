using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public class Address
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; private set; } = Guid.NewGuid();
    [MaxLength(200)]

    public string Street { get; private set; }
    [MaxLength(100)]

    public string City { get; private set; } = "Oslo";
    [MaxLength(20)]
    public string ZipCode { get; private set; }
    [MaxLength(100)]
    public string Country { get; private set; } = "Norway";
    [ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    private Address() { }
    public Address(string street, string zipCode, Guid customerId)
    {
        Street = street;
        City = "Oslo";
        ZipCode = zipCode;
        CustomerId = customerId;
        Country = "Norway";
    }
}