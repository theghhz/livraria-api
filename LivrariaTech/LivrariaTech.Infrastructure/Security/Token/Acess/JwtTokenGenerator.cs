using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using LivrariaTech.Domain.Entities;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace LivrariaTech.Infrastructure.Security.Token.Acess;

public class JwtTokenGenerator
{   
    private readonly string _key;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _key = configuration["JwtSettings:Secret"] ?? throw new ArgumentNullException("JWT Secret not found.");
    }
    
    public string GenerateToken(User user)
    {   
        var claim = new List<Claim>(){
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = new ClaimsIdentity(claim),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private SymmetricSecurityKey SecurityKey()
    {   
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
    }
}
