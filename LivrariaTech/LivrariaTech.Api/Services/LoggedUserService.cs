using System.IdentityModel.Tokens.Jwt;
using LivrariaTech.Domain.Entities;
using LivrariaTech.Infrastructure;
using LivrariaTech.UseCases.UseCases.Checkouts.Interfaces;

namespace LivrariaTech.Api.Services;

public class LoggedUserService : IUserService
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly LivrariaTechDbContext _dbContext;
    
    public LoggedUserService(IHttpContextAccessor httpContext, LivrariaTechDbContext dbContext) => (_httpContext, _dbContext) = (httpContext, dbContext);
    
    public Guid GetLoggedUserId()
    {   
        var auth = _httpContext.HttpContext.Request.Headers.Authorization.ToString();

        var token = auth["Bearer ".Length..].Trim();

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var identifier = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
        
        return Guid.Parse(identifier);
    }

    public User User()
    {   
        var userId = GetLoggedUserId();
        return _dbContext.Users.First(user => user.Id == userId);
    }
    
}