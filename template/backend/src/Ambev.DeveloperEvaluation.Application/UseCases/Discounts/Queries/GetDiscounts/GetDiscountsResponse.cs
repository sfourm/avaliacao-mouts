using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Enums;
using Ambev.DeveloperEvaluation.Packages.Application.Models;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Queries.GetDiscounts;

public class GetDiscountsResponse : PagingResult<GetDiscountsResponseData>;

public sealed class GetDiscountsResponseData
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Code { get; init; } = null!;
    public decimal Value { get; init; }
    public DiscountType Type { get; init; }
    public DateTime StartDt { get; init; }
    public DateTime EndDt { get; init; }
    public bool IsActive { get; init; }
    public int? MaxUses { get; init; }
    public int? MinOrderValue { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}