using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUser;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<User, GetUserResult>();
    }
}
