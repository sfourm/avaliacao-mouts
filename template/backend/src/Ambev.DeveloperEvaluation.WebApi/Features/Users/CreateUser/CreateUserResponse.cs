using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// API response model for CreateUser operation
/// </summary>
public sealed record CreateUserResponse
{
    /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The user's full name
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The user's email address
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// The user's phone number
    /// </summary>
    public string Phone { get; init; } = string.Empty;

    /// <summary>
    /// The user's role in the system
    /// </summary>
    public UserRole Role { get; init; }

    /// <summary>
    /// The current status of the user
    /// </summary>
    public UserStatus Status { get; init; }
}
