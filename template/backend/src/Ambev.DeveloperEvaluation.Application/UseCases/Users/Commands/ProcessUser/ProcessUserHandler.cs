using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.ProcessUser;

public sealed class ProcessUserHandler : IRequestHandler<ProcessUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProcessUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ProcessUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new NotFoundException("The user was not found.");
        
        user.Activate();
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}