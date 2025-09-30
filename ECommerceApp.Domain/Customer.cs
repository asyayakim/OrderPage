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
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; private set; }
    [JsonIgnore]
    public Address Address { get; private set; } 
    public DateOnly Birthday { get; private set; }
    
    public Customer(string name, string email, DateOnly birthday, Guid dtoUserId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Birthday = birthday;
    }
    private Customer() { } 
    public static Customer Create(Guid userId, string name, string email, DateOnly age)
    {
        var customer = new Customer(name, email, age, userId)
        {
            UserId = userId,
            Id = userId,
            Birthday = age
        };       
        return customer;
    }
}