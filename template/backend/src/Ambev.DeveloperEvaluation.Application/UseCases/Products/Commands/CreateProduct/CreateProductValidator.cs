using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Commands.CreateProduct;

public sealed class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Product.MaxLengthName);

        RuleFor(x => x.Description)
            .MaximumLength(Product.MaxLengthDescription);

        RuleFor(x => x.Sku)
            .NotEmpty()
            .MaximumLength(Product.MaxLengthSku);

        RuleFor(x => x.Barcode)
            .NotEmpty()
            .MaximumLength(Product.MaxLengthBarcode);

        RuleFor(x => x.PriceAmount)
            .GreaterThan(0);

        RuleFor(x => x.PriceCurrency)
            .NotEmpty()
            .Length(3);

        RuleFor(x => x.Status)
            .IsInEnum();

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0);
    }
}