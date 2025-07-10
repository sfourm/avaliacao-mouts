using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;
using Riok.Mapperly.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Queries.GetDiscounts;

[Mapper]
public static partial class GetDiscountsMapper
{
    public static partial GetDiscountsResponseData ToGetDiscountsResponseData(this Discount discount);
}