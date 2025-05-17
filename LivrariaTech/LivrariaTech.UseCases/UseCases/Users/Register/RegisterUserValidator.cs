
using FluentValidation;
using LivrariaTech.Comunication.Requests;

namespace LivrariaTech.UseCases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is invalid");

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage("Password is required");

        When(request => string.IsNullOrEmpty(request.Password) == false , () =>
        {
            RuleFor(request => request.Password.Length).GreaterThanOrEqualTo(6)
                .WithMessage("Password is required to be at least 6 characters long");
        });
    }
}