using Ambev.DeveloperEvaluation.Common.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Repositories;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.DeleteUser;

/// <summary>
/// Handler for processing DeleteUserCommand requests
/// </summary>
public sealed class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of DeleteUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="unitOfWork"></param>
    public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the DeleteUserCommand request
    /// </summary>
    /// <param name="request">The DeleteUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var existsUser = await _userRepository.ExistsAsync(request.Id, cancellationToken);
        
        if (!existsUser)
        {
            throw new NotFoundException($"User with id {request.Id} not found");
        }
        
        await _userRepository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new DeleteUserResponse(true);
    }
}
