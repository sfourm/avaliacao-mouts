using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Commands.UpdateDiscount;

public sealed class UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDiscountHandler(IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
    {
        _discountRepository = discountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateDiscountCommand command, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetByIdAsync(command.Id, cancellationToken)
                       ?? throw new NotFoundException("Discount was not found.");

        discount.Update(
            command.Name,
            command.Description,
            command.Code,
            command.Value,
            command.Type,
            command.StartDt,
            command.EndDt,
            command.IsActive,
            command.MaxUses,
            command.MinOrderValue);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}