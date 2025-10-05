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

    [JsonIgnore]
    public Address Address { get; private set; } 
    public DateOnly Birthday { get; private set; }
    
    public Customer(Guid userId, DateOnly birthday)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Birthday = birthday;
    }
    private Customer() { } 
    public static Customer Create(Guid userId, DateOnly age)
    {
        var customer = new Customer(userId, age)
        {
            UserId = userId,
            
            Birthday = age
        };       
        return customer;
    }
}