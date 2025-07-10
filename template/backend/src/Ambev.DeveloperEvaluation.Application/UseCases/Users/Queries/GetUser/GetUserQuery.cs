using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUser;

public sealed record GetUserQuery(Guid Id) : IRequest<GetUserResponse>;