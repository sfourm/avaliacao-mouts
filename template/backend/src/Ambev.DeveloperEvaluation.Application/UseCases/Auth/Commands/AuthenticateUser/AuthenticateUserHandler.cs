using Ambev.DeveloperEvaluation.Application.Common.Interfaces.Services;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Auth.Commands.AuthenticateUser;

public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResponse>
{
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;
    private readonly IUserRepository _userRepository;

    public AuthenticateUserHandler(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<AuthenticateUserResponse> Handle(AuthenticateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null || !_passwordService.VerifyPassword(request.Password, user.Password))
            throw new UnauthorizedAccessException("Invalid credentials");

        if (user.IsSatisfiedBy()) throw new UnauthorizedAccessException("User is not active");

        var token = _jwtService.GenerateToken(user);

        return new AuthenticateUserResponse(token);
    }
}