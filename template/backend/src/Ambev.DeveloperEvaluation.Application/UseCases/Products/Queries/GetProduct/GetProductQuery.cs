using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Queries.GetProduct;

public sealed record GetProductQuery(Guid Id) : IRequest<GetProductResponse>;