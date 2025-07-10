using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Enums;
using Ambev.DeveloperEvaluation.Packages.Application.Models;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Queries.GetProducts;

public sealed class GetProductsQuery : PagingRequest, IRequest<GetProductsResponse>
{
    public ProductStatus? Status { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public int? MinStock { get; init; }
}