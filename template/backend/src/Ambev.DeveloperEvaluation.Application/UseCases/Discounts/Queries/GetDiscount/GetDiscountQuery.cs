using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Queries.GetDiscount;

public sealed record GetDiscountQuery(Guid Id) : IRequest<GetDiscountResponse>;