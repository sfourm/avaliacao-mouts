using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Auth.Commands.AuthenticateUser;

public sealed class AuthenticateUserCommand : IRequest<AuthenticateUserResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}