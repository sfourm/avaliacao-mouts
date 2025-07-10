using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Entities;
using Riok.Mapperly.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Queries.GetProducts;

[Mapper]
public static partial class GetProductsMapper
{
    public static partial GetProductsResponseData ToGetProductsResponseData(this Product product);
}