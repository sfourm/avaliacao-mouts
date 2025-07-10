using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Queries.GetDiscount;

public sealed class GetDiscountHandler : IRequestHandler<GetDiscountQuery, GetDiscountResponse>
{
    private readonly IDiscountRepository _discountRepository;

    public GetDiscountHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<GetDiscountResponse> Handle(GetDiscountQuery command, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetByIdAsync(command.Id, cancellationToken)
                       ?? throw new NotFoundException("The discount was not found.");

        return discount.ToGetDiscountByIdResponse();
    }
}