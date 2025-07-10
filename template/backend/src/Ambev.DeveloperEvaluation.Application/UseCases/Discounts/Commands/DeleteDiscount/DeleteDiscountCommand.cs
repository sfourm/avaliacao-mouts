using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Commands.DeleteDiscount;

public sealed record DeleteDiscountCommand(Guid Id) : IRequest;