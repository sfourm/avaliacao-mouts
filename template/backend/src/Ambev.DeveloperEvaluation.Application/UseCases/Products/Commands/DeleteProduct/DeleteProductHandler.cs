using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Products.Commands.DeleteProduct;

public sealed class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken) 
                      ?? throw new NotFoundException("Product not found");

        await _productRepository.DeleteAsync(product.Id, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}