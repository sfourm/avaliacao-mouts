using System.Net;
using System.Net.Mime;
using Ambev.DeveloperEvaluation.Application.UseCases.Products.Commands.CreateProduct;
using Ambev.DeveloperEvaluation.Application.UseCases.Products.Commands.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.UseCases.Products.Commands.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.UseCases.Products.Queries.GetProduct;
using Ambev.DeveloperEvaluation.Application.UseCases.Products.Queries.GetProducts;
using Ambev.DeveloperEvaluation.Packages.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers.v1;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/products")]
public class ProductsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="command">The product creation data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created product details.</returns>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, response);
    }

    /// <summary>
    /// Deletes an existing product.
    /// </summary>
    /// <param name="productId">The product ID to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    [HttpDelete("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(
        [FromRoute] Guid productId,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteProductCommand(productId), cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="productId">The product ID to update.</param>
    /// <param name="command">The product update data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    [HttpPut("{productId:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid productId,
        [FromBody] UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command with { Id = productId }, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Gets a product by its ID.
    /// </summary>
    /// <param name="productId">The product ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The product details.</returns>
    [HttpGet("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(
        [FromRoute] Guid productId,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductQuery(productId), cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Searches for products with optional filters.
    /// </summary>
    /// <param name="query">The search criteria.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated list of products.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(
        [FromQuery] GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
}