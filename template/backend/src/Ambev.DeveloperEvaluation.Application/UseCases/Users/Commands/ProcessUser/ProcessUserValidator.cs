using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.ProcessUser;

public sealed class ProcessUserValidator : AbstractValidator<ProcessUserCommand>
{
    public ProcessUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}