using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Infrastructure.EF.Context;

namespace Ambev.DeveloperEvaluation.Infrastructure.Persistences.Repositories.Products;

public class ProductRepository(DefaultContext context) : BaseRepository<Product>(context), IProductRepository;