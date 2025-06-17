using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.Domain;
using Microsoft.AspNetCore.Identity;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class UserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<object> SaveToDbAsync(CreateUserDto userDto)
    {
        var newUser = new UserData
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            Age = userDto.Age,
        };
        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();
        return newUser;
    }
}