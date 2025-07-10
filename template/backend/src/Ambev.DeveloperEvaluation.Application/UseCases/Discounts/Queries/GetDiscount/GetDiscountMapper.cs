using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;
using Riok.Mapperly.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Queries.GetDiscount;

[Mapper]
public static partial class GetDiscountMapper
{
    public static partial GetDiscountResponse ToGetDiscountByIdResponse(this Discount discount);
}