using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Commands.CreateProduct;

public sealed class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var price = new Price(command.PriceAmount, command.PriceCurrency);
        
        var product = new Product(
            command.Name,
            command.Description,
            command.Sku,
            command.Barcode,
            price,
            command.Status,
            command.StockQuantity);

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CreateProductResponse(product.Id);
    }
}