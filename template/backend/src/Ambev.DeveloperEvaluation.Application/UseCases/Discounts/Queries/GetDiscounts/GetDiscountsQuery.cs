using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Enums;
using Ambev.DeveloperEvaluation.Packages.Application.Models;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Queries.GetDiscounts;

public sealed class GetDiscountsQuery : PagingRequest, IRequest<GetDiscountsResponse>
{
    public DiscountType? Type { get; init; }
    public bool? IsActive { get; init; }
    public DateTime? StartDt { get; init; }
    public DateTime? EndDt { get; init; }
}