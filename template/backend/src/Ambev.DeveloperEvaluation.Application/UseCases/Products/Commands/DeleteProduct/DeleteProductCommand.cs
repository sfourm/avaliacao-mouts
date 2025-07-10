using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : IRequest;