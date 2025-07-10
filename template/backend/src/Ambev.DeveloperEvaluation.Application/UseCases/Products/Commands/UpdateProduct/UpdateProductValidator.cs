using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Commands.UpdateProduct;

public sealed class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required")
            .MaximumLength(Product.MaxLengthName)
            .WithMessage($"Product name must be less than {Product.MaxLengthName} characters");

        RuleFor(x => x.Description)
            .MaximumLength(Product.MaxLengthDescription)
            .WithMessage($"Description must be less than {Product.MaxLengthDescription} characters");

        RuleFor(x => x.Sku)
            .NotEmpty()
            .WithMessage("SKU is required")
            .MaximumLength(Product.MaxLengthSku)
            .WithMessage($"SKU must be less than {Product.MaxLengthSku} characters");

        RuleFor(x => x.Barcode)
            .NotEmpty()
            .WithMessage("Barcode is required")
            .MaximumLength(Product.MaxLengthBarcode)
            .WithMessage($"Barcode must be less than {Product.MaxLengthBarcode} characters");

        RuleFor(x => x.PriceAmount)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero");

        RuleFor(x => x.PriceCurrency)
            .NotEmpty()
            .WithMessage("Currency is required")
            .Length(3)
            .WithMessage("Currency must be 3 characters long (ISO code)");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid product status");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity cannot be negative");
    }
}