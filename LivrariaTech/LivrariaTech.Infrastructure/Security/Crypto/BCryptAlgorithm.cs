namespace LivrariaTech.Infrastructure.Security.Crypto;

using BCrypt.Net;
using LivrariaTech.Domain.Entities;

public class BCryptAlgorithm
{
    public string HashPassword(string password)
    {
        return BCrypt.HashPassword(password);
    }

    public bool Verify(string password, User user)
    {
        return BCrypt.Verify(password, user.Password);
    }
}