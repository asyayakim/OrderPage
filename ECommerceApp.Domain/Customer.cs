using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [ForeignKey(nameof(UserData))]
    public Guid UserId { get; private set; } = Guid.NewGuid();
    public virtual UserData User { get; set; }
    [MaxLength(200)]
    public string Name { get; private set; }
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; private set; }
    public Address Address { get; private set; }
    [Range(1, 120)]
    public int Age { get; private set; }
    
    public Customer(string name, string email, Address address, int age)
    {
        Name = name;
        Email = email;
        Address = address;
        Age = age;
    }
    private Customer() { } 
}