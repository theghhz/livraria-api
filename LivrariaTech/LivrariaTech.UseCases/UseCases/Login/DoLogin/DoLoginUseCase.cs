using LivrariaTech.Comunication.Requests;
using LivrariaTech.Comunication.Responses;
using LivrariaTech.Exception.Exception;
using LivrariaTech.Infrastructure;
using LivrariaTech.Infrastructure.Security.Crypto;
using LivrariaTech.Infrastructure.Security.Token.Acess;

namespace LivrariaTech.UseCases.Login.DoLogin;

public class DoLoginUseCase{
    
    private readonly JwtTokenGenerator _tokenGenerator;
    private readonly LivrariaTechDbContext _dbContext;

    public DoLoginUseCase(JwtTokenGenerator tokenGenerator, LivrariaTechDbContext dbContext)
    {
        _tokenGenerator = tokenGenerator;
        _dbContext = dbContext;
    }
    
    public ResponseRegisteresUserJson Execute(RequestLoginJson request){
        
        var user = _dbContext.Users.FirstOrDefault(user => user.Email == request.Email);

        if(user is null)
        {
            throw new InvalidLoginException(string.Empty);
        }

        var cryptography = new BCryptAlgorithm();
        var passwordIsValid = cryptography.Verify(request.Password, user);
        
        if(passwordIsValid == false)
        {
            throw new InvalidLoginException(string.Empty);
        }
        
        return new ResponseRegisteresUserJson
        {
            Name = user.Name,
            AcessToken = _tokenGenerator.GenerateToken(user),
        };
    }
}