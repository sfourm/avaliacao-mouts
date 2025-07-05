using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.DeleteUser;

/// <summary>
/// Command for deleting a user
/// </summary>
public sealed record DeleteUserCommand(Guid Id) : IRequest<DeleteUserResponse>;
