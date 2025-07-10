using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Queries.GetDiscounts;

public sealed class GetDiscountsQuery : IRequest<GetDiscountsResponse>
{
    public DiscountType? Type { get; init; }
    public bool? IsActive { get; init; }
    public DateTime? StartDt { get; init; }
    public DateTime? EndDt { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}