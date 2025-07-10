using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUser;

public sealed class GetUserValidator : AbstractValidator<GetUserQuery>
{
    public GetUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}