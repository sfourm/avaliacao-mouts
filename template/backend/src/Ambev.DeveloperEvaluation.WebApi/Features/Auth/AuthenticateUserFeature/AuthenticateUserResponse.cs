using Ambev.DeveloperEvaluation.Packages.WebApi.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;

/// <summary>
/// Represents the response returned after user authentication
/// </summary>
public sealed record AuthenticateUserResponse
{
    /// <summary>
    /// Gets or sets the JWT token for authenticated user
    /// </summary>
    public string Token { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's email address
    /// </summary>
    public string Email { get; init; } = string.Empty;   

    /// <summary>
    /// Gets or sets the user's full name
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's role in the system
    /// </summary>
    public string Role { get; init; } = string.Empty;
}
