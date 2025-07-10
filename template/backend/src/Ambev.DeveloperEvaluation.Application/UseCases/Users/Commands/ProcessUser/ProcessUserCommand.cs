using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.ProcessUser;

public sealed record ProcessUserCommand(Guid Id): IRequest;