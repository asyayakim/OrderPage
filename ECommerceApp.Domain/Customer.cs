namespace ECommerceApp.Domain;

public class Customer
{
    private Customer() { } 
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Address Address { get; private set; }
    
    public Customer(string name, string email, Address address)
    {
        Name = name;
        Email = email;
    }
}