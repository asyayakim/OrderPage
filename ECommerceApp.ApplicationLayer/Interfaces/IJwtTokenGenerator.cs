using ECommerceApp.Domain;

namespace ECommerceApp.ApplicationLayer.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(UserData user, IList<string> roles);
}
