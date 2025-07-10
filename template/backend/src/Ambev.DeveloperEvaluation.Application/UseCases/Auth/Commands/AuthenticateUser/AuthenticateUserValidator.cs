using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Auth.Commands.AuthenticateUser;

public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}