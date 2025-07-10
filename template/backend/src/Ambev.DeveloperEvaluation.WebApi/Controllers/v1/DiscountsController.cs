using System.Net;
using System.Net.Mime;
using Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Commands.CreateDiscount;
using Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Commands.DeleteDiscount;
using Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Commands.UpdateDiscount;
using Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Queries.GetDiscount;
using Ambev.DeveloperEvaluation.Application.UseCases.Discounts.Queries.GetDiscounts;
using Ambev.DeveloperEvaluation.Packages.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers.v1;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/discounts")]
public class DiscountsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public DiscountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new discount.
    /// </summary>
    /// <param name="command">The discount creation data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created discount details.</returns>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, response);
    }

    /// <summary>
    /// Deletes an existing discount.
    /// </summary>
    /// <param name="discountId">The discount ID to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    [HttpDelete("{discountId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDiscount(
        [FromRoute] Guid discountId,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteDiscountCommand(discountId), cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Updates an existing discount.
    /// </summary>
    /// <param name="discountId">The discount ID to update.</param>
    /// <param name="command">The discount update data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated discount details.</returns>
    [HttpPut("{discountId:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDiscount(
        [FromRoute] Guid discountId,
        [FromBody] UpdateDiscountCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command with { Id = discountId }, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Gets a discount by its ID.
    /// </summary>
    /// <param name="discountId">The discount ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The discount details.</returns>
    [HttpGet("{discountId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDiscountById(
        [FromRoute] Guid discountId,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetDiscountQuery(discountId), cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Searches for discounts with optional filters.
    /// </summary>
    /// <param name="query">The search criteria.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated list of discounts.</returns>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchDiscounts(
        [FromQuery] GetDiscountsQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
}