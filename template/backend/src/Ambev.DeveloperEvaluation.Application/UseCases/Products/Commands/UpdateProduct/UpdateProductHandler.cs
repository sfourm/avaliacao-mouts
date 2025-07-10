using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Commands.UpdateProduct;

public sealed class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken) 
                      ?? throw new KeyNotFoundException("Product not found");

        var price = new Price(command.PriceAmount, command.PriceCurrency);

        product.Update(
            command.Name,
            command.Description,
            command.Sku,
            command.Barcode,
            price,
            command.Status,
            command.StockQuantity);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}