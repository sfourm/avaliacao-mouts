using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Infrastructure.Messaging.Bases;

public class MessageProducer<TMessage> : IMessageProducer<TMessage> where TMessage : IEvent
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessageProducer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task ProduceAsync(TMessage message, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(message, cancellationToken);
    }
}