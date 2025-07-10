using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using MassTransit;
using MediatR;

namespace Ambev.DeveloperEvaluation.Infrastructure.Messaging.Bases;

public abstract class MessageConsumer<TMessage> : IMessageConsumer<TMessage>, IConsumer<TMessage>
    where TMessage : class, IEvent
{
    private readonly IMediator _mediator;

    protected MessageConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<TMessage> context)
    {
        await HandleMessageAsync(context.Message, context.CancellationToken);
    }

    public abstract Task HandleMessageAsync(TMessage message, CancellationToken cancellationToken);
}