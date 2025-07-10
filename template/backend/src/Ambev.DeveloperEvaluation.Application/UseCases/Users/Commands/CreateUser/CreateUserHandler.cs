using Ambev.DeveloperEvaluation.Application.Common.Interfaces.Services;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.CreateUser;

public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IPasswordService _passwordService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository, IPasswordService passwordService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        await ValidateUser(command, cancellationToken);

        var password = _passwordService.HashPassword(command.Password);
        var user = CreateUser(command, password);

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CreateUserResponse(user.Id);
    }

    private async Task ValidateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (existingUser != null)
            throw new InvalidOperationException($"User with email {command.Email} already exists");
    }

    private static User CreateUser(CreateUserCommand command, Password password)
    {
        var name = new Name(command.FirstName, command.LastName);
        var email = new Email(command.Email);
        var phone = new Phone(command.Idd, command.Phone);
        var username = new Username(command.Username);

        return new User(name, email, phone, username, password, command.Role, command.Status);
    }
}