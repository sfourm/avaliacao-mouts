using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUser;

public sealed class GetUserHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException("The user was not found");

        return _mapper.Map<GetUserResponse>(user);
    }
}