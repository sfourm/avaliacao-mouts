using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Enums;
using Ambev.DeveloperEvaluation.Packages.Application.Models;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUsers;

public sealed class GetUsersQuery : PagingRequest, IRequest<GetUsersResponse>
{
    public UserRole? Role { get; init; }
    public UserStatus? Status { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}