using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Commands.CreateDiscount;

public sealed record CreateDiscountCommand : IRequest<CreateDiscountResponse>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public decimal Value { get; init; }
    public DiscountType Type { get; init; }
    public DateTime StartDt { get; init; }
    public DateTime EndDt { get; init; }
    public bool IsActive { get; init; }
    public int? MaxUses { get; init; }
    public int? MinOrderValue { get; init; }
    public bool? IsPublic { get; init; }
}