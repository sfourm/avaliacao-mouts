using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUsers;

public class GetUsersValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersValidator()
    {
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
            .WithMessage("A data final deve ser maior ou igual à data inicial");
            
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .When(x => x.StartDate.HasValue)
            .WithMessage("A data inicial não pode ser no futuro");
    }
}