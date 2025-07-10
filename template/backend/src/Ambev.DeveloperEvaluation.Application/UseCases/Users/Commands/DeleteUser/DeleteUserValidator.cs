using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.DeleteUser;

public sealed class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}