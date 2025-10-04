using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceApp.Domain;

public class Customer
{
    [Key]
    public Guid Id { get; private set; }
    
    [ForeignKey("User")]
    public Guid UserId { get; set; } 
    
    [MaxLength(200)]
    public string Name { get; private set; }

    [JsonIgnore]
    public Address Address { get; private set; } 
    public DateOnly Birthday { get; private set; }
    
    public Customer(Guid userId, string name, DateOnly birthday)
    {
        Id = Guid.NewGuid();
        Name = name;
        Birthday = birthday;
    }
    private Customer() { } 
    public static Customer Create(Guid userId, string name, DateOnly age)
    {
        var customer = new Customer(userId, name, age)
        {
            UserId = userId,
            Name = name,
            Birthday = age
        };       
        return customer;
    }
}