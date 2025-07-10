using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Commands.CreateDiscount;

public sealed class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CreateDiscountResponse>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiscountHandler(IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
    {
        _discountRepository = discountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateDiscountResponse> Handle(CreateDiscountCommand command, CancellationToken cancellationToken)
    {
        Discount discount = new(
            command.Name,
            command.Description,
            command.Code,
            command.Value,
            command.Type,
            command.StartDt,
            command.EndDt,
            command.IsActive,
            command.MaxUses,
            command.MinOrderValue,
            command.IsPublic);

        await _discountRepository.AddAsync(discount, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CreateDiscountResponse(discount.Id);
    }
}