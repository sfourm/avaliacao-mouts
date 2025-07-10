using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand : IRequest
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Sku { get; init; } = string.Empty;
    public string Barcode { get; init; } = string.Empty;
    public decimal PriceAmount { get; init; }
    public string PriceCurrency { get; init; } = "BRL";
    public ProductStatus Status { get; init; }
    public int StockQuantity { get; init; }
}