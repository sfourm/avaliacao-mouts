using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid Id) : IRequest;