using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Common.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .Must(BeAValidPassword)
            .WithMessage("Invalid password format.");
    }

    private static bool BeAValidPassword(string password)
    {
        try
        {
            Password.ValidatePasswordRules(password);
            return true;
        }
        catch (RuleViolationDomainException ex)
        {
            throw new ValidationException(ex.Message);
        }
    }
}