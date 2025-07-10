using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using Riok.Mapperly.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUser;

[Mapper]
public static partial class GetUserMapper
{
    public static partial GetUserResponse ToGetUserQuery(this User user);
}