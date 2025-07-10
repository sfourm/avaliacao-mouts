using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using Riok.Mapperly.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUsers;

[Mapper]
public static partial class GetUsersMapper
{
    public static partial GetUsersResponseData ToGetUsersResponseData(this User user);
}