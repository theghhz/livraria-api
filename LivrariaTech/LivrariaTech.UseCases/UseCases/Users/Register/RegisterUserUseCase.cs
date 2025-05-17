using LivrariaTech.Comunication.Requests;
using LivrariaTech.Comunication.Responses;
using LivrariaTech.Exception.Exception;
using LivrariaTech.Domain.Entities;
using LivrariaTech.Infrastructure;
using LivrariaTech.Infrastructure.Security.Crypto;
using FluentValidation;
using FluentValidation.Results;
using LivrariaTech.Infrastructure.Security.Token.Acess;

namespace LivrariaTech.UseCases.Users.Register;

public class RegisterUserUseCase
{   
    private readonly JwtTokenGenerator _tokenGenerator;
    private readonly LivrariaTechDbContext _dbContext;
    
    public RegisterUserUseCase(JwtTokenGenerator tokenGenerator, LivrariaTechDbContext dbContext)
    {
        _tokenGenerator = tokenGenerator;
        _dbContext = dbContext;
    }
    public ResponseRegisteresUserJson Execute(RequestUserJson request)
    {
        ValidateRequest(request, _dbContext);

        var cryptography = new BCryptAlgorithm();

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = cryptography.HashPassword(request.Password),
        };
        
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        
        
        return new ResponseRegisteresUserJson
        {
            Name = user.Name,
            AcessToken = _tokenGenerator.GenerateToken(user),
        };
    }

    private void ValidateRequest(RequestUserJson request, LivrariaTechDbContext dbContext)
    {
        var validator = new RegisterUserValidator();
        var validationResult = validator.Validate(request);

        var existUser = dbContext.Users.Any(user => user.Email.Equals(request.Email));

        if(existUser)
        {
            validationResult.Errors.Add(new ValidationFailure("Email", "Email already exists"));
        }

        if (validationResult.IsValid == false)
        {
            var errosMessages = validationResult.Errors.Select(request => request.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errosMessages);
        }
    }
}
