using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Entities;
using Riok.Mapperly.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Queries.GetProduct;

[Mapper]
public static partial class GetProductMapper
{
    public static partial GetProductResponse ToGetProductResponse(this Product product);
}