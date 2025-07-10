using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUser;

public sealed class GetUserProfile : Profile
{
    public GetUserProfile()
    {
        CreateMap<User, GetUserResponse>();
    }
}