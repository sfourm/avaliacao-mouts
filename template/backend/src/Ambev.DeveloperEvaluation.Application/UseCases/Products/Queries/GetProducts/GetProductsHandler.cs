using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Interfaces;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Queries.GetProducts;

public sealed class GetProductsHandler : IRequestHandler<GetProductsQuery, GetProductsResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<GetProductsResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var filter = CreateFilter(query);
        var products = _productRepository.QueryableComplete(query.PageNumber, query.PageSize, filter);
        var data = products.Select(x => x.ToGetProductsResponseData()).ToList();
       
        var result = new GetProductsResponse
        { 
            Data = data, 
            Hits = data.Count
        };
        
        return Task.FromResult(result);
    }

    private static Expression<Func<Product, bool>> CreateFilter(GetProductsQuery query)
    {
        return product =>
            (query.Status == null || product.Status == query.Status) &&
            (query.MinPrice == null || product.Price.Amount >= query.MinPrice) &&
            (query.MaxPrice == null || product.Price.Amount <= query.MaxPrice) &&
            (query.MinStock == null || product.StockQuantity >= query.MinStock);
    }
}