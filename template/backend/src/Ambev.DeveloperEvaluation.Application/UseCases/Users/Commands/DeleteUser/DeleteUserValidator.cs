using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.DeleteUser;

/// <summary>
/// Validator for DeleteUserCommand
/// </summary>
public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
