using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Interfaces;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Queries.GetDiscounts;

public sealed class GetDiscountsHandler : IRequestHandler<GetDiscountsQuery, GetDiscountsResponse>
{
    private readonly IDiscountRepository _discountRepository;

    public GetDiscountsHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public Task<GetDiscountsResponse> Handle(GetDiscountsQuery query, CancellationToken cancellationToken)
    {
        var filter = CreateFilter(query);
        var discounts = _discountRepository.QueryableComplete(query.PageNumber, query.PageSize, filter);
        var data = discounts.Select(x => x.ToGetDiscountsResponseData()).ToList();

        return Task.FromResult(new GetDiscountsResponse { Data = data, Hits = data.Count });
    }

    private static Expression<Func<Discount, bool>> CreateFilter(GetDiscountsQuery query)
    {
        return discount =>
            (query.Type == null || discount.Type == query.Type) &&
            (query.IsActive == null || discount.IsActive == query.IsActive) &&
            (query.StartDt == null || discount.StartDt >= query.StartDt) &&
            (query.EndDt == null || discount.EndDt <= query.EndDt);
    }
}