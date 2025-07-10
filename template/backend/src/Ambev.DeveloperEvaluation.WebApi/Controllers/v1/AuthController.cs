using Ambev.DeveloperEvaluation.Application.UseCases.Auth.Commands.AuthenticateUser;
using Ambev.DeveloperEvaluation.Packages.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers.v1;

/// <summary>
///     Controller for authentication operations
/// </summary>
[ApiController]
[Route("v{version:apiVersion}/auth")]
public class AuthController : ApiControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Initializes a new instance of AuthController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Authenticates a user with their credentials
    /// </summary>
    /// <param name="command">The authentication request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Authentication token if successful</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AuthenticateUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }
}