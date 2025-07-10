using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Commands.DeleteDiscount;

public sealed class DeleteDiscountHandler : IRequestHandler<DeleteDiscountCommand>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDiscountHandler(IDiscountRepository discountRepository,
        IUnitOfWork unitOfWork)
    {
        _discountRepository = discountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteDiscountCommand command, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetByIdAsync(command.Id, cancellationToken)
                       ?? throw new NotFoundException("The discount was not found.");

        await _discountRepository.DeleteAsync(discount.Id, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}