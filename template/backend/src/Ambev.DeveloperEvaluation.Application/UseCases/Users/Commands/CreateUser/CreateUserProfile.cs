using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public sealed class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<User, CreateUserResult>();
    }
}
