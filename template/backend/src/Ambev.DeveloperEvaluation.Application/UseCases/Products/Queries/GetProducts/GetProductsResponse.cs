using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Enums;
using Ambev.DeveloperEvaluation.Packages.Application.Models;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Queries.GetProducts;

public class GetProductsResponse : PagingResult<GetProductsResponseData>;

public sealed class GetProductsResponseData
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Sku { get; init; } = null!;
    public string Barcode { get; init; } = null!;
    public decimal PriceAmount { get; init; }
    public string PriceCurrency { get; init; } = null!;
    public ProductStatus Status { get; init; }
    public int StockQuantity { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}