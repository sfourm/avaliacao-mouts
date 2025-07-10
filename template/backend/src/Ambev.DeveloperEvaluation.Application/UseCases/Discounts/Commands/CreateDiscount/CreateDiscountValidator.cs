using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Commands.CreateDiscount;

public sealed class CreateDiscountValidator : AbstractValidator<CreateDiscountCommand>
{
    public CreateDiscountValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Discount.MaxLengthName);

        RuleFor(x => x.Description)
            .MaximumLength(Discount.MaxLengthDescription);

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(Discount.MaxLengthCode);

        RuleFor(x => x.Value)
            .GreaterThan(0);

        RuleFor(x => x.Type)
            .IsInEnum();

        RuleFor(x => x.StartDt)
            .NotEmpty()
            .GreaterThan(DateTime.UtcNow.AddMinutes(-5))
            .WithMessage("Start date must be in the future");

        RuleFor(x => x.EndDt)
            .NotEmpty()
            .GreaterThan(x => x.StartDt)
            .WithMessage("End date must be after start date");

        When(x => x.MaxUses.HasValue, () =>
        {
            RuleFor(x => x.MaxUses)
                .GreaterThan(0);
        });

        When(x => x.MinOrderValue.HasValue, () =>
        {
            RuleFor(x => x.MinOrderValue)
                .GreaterThan(0);
        });
    }
}