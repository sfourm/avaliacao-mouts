using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUser;

public sealed class GetUserHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException("The user was not found");
        
        return user.ToGetUserQuery();
    }
}