using Ambev.DeveloperEvaluation.Application.UseCases.Users.Commands.ProcessUser;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Events;
using Ambev.DeveloperEvaluation.Infrastructure.Messaging.Bases;
using MediatR;

namespace Ambev.DeveloperEvaluation.Infrastructure.Messaging.Consumers;

public sealed class UserRegisteredConsumer(IMediator mediator) : MessageConsumer<UserRegisteredEvent>(mediator)
{
    private readonly IMediator _mediator = mediator;

    public override async Task HandleMessageAsync(UserRegisteredEvent message,
        CancellationToken cancellationToken)
    {
        ProcessUserCommand command = new(message.Id);

        await _mediator.Send(command, cancellationToken);
    }
}