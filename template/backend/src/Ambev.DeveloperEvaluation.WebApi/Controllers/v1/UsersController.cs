using Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.CreateUser;
using Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.DeleteUser;
using Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUser;
using Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUsers;
using Ambev.DeveloperEvaluation.Packages.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers.v1;

/// <summary>
///     Controller for managing user operations
/// </summary>
[ApiController]
[Route("v{version:apiVersion}/users")]
public class UsersController : ApiControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Initializes a new instance of UsersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Creates a new user
    /// </summary>
    /// <param name="command">The user creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, response);
    }

    /// <summary>
    ///     Retrieves a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUserQuery(id), cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    ///     Retrieves a list of all users.
    /// </summary>
    /// <remarks>
    ///     This endpoint allows you to retrieve a list of all users in the system.
    /// </remarks>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="query">The search criteria, including optional date range and pagination parameters.</param>
    /// <returns>A list of all users.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    ///     Deletes a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the user was deleted</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUserCommand(id), cancellationToken);

        return NoContent();
    }
}