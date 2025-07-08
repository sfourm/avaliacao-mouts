using Ambev.DeveloperEvaluation.Application.Common.Interfaces;
using Ambev.DeveloperEvaluation.Application.Common.Interfaces.Services;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Repositories;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.CreateUser;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordService passwordService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordService = passwordService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
       await ValidateUser(command, cancellationToken);

        var user = _mapper.Map<User>(command);
        var password = _passwordService.HashPassword(command.Password);
        user.SetPassword(password);

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new CreateUserResult(user.Id);
    }

    private async Task ValidateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
        
        if (existingUser != null)
            throw new InvalidOperationException($"User with email {command.Email} already exists");
    }
}
