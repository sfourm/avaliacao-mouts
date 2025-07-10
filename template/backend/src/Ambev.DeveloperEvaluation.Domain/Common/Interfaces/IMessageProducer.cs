namespace Ambev.DeveloperEvaluation.Domain.Common.Interfaces;

public interface IMessageProducer<in TMessage> where TMessage : IEvent
{
    Task ProduceAsync(TMessage message, CancellationToken cancellationToken);
}