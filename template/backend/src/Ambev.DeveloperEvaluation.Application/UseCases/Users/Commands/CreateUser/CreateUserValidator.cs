using Ambev.DeveloperEvaluation.Application.Common.Interfaces.Services;
using Ambev.DeveloperEvaluation.Application.Common.Validators;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IPhoneService phoneService)
    {
        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MaximumLength(Name.MaxFirstNameLength);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(Name.MaxLastNameLength);

        RuleFor(command => command.Username)
            .NotEmpty()
            .Length(Username.MinLength, Username.MaxLength)
            .Must(Username.IsValidUsername)
            .WithMessage("Username contains invalid characters.");

        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator());

        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(Email.MaxEmailLength);

        RuleFor(command => command.Idd)
            .NotEmpty()
            .MaximumLength(Phone.MaxIddLength)
            .Must(idd => idd.All(char.IsDigit))
            .WithMessage("Idd must contain only digits.");

        RuleFor(command => command.Phone)
            .NotEmpty()
            .MaximumLength(Phone.MaxNumberLength)
            .Must(phone => phone.All(char.IsDigit))
            .WithMessage("Phone number must contain only digits.")
            .Must((command, phone) => phoneService.IsValid(command.Idd, phone))
            .WithMessage("Invalid phone format.");

        RuleFor(command => command.Status)
            .NotEqual(UserStatus.Unknown);

        RuleFor(command => command.Role)
            .NotEqual(UserRole.None);
    }
}