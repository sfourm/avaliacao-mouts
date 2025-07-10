using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Queries.GetProduct;

public sealed class GetProductHandler : IRequestHandler<GetProductQuery, GetProductResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductResponse> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(query.Id, cancellationToken)
                      ?? throw new NotFoundException("The product was not found.");

        return product.ToGetProductResponse();
    }
}