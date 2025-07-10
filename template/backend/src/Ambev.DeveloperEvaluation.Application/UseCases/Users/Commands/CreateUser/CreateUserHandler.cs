using Ambev.DeveloperEvaluation.Application.Common.Interfaces.Services;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Enums;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Events;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.CreateUser;

public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IPasswordService _passwordService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMessageProducer<UserRegisteredEvent> _messageProducer;

    public CreateUserHandler(IUserRepository userRepository, IPasswordService passwordService, IUnitOfWork unitOfWork, 
        IMessageProducer<UserRegisteredEvent> messageProducer)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _unitOfWork = unitOfWork;
        _messageProducer = messageProducer;
    }

    public async Task<CreateUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        await ValidateUser(command, cancellationToken);

        var user = CreateUser(command);

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        await PublishEvent(user.Id, cancellationToken);
        
        return new CreateUserResponse(user.Id);
    }

    private async Task ValidateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (existingUser != null)
        {
            throw new AlreadyExistsException($"User with email {command.Email} already exists");
        }
    }

    private User CreateUser(CreateUserCommand command)
    {
        var name = new Name(command.FirstName, command.LastName);
        var email = new Email(command.Email);
        var phone = new Phone(command.Idd, command.Phone);
        var username = new Username(command.Username);
        var password = _passwordService.HashPassword(command.Password);

        return new User(name, email, phone, username, password, command.Role, UserStatus.Inactive);
    }

    private async Task PublishEvent(Guid id, CancellationToken cancellationToken)
    {
        var message = new UserRegisteredEvent { Id = id };
        await _messageProducer.ProduceAsync(message, cancellationToken);
    }
}