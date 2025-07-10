using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Commands.DeleteDiscount;

public sealed class DeleteDiscountValidator : AbstractValidator<DeleteDiscountCommand>
{
    public DeleteDiscountValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}