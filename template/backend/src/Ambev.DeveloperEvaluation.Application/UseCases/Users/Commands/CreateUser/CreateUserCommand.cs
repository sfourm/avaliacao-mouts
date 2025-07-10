using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.CreateUser;

public sealed record CreateUserCommand : IRequest<CreateUserResponse>
{
    public string Username { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Idd { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public UserRole Role { get; init; }
}