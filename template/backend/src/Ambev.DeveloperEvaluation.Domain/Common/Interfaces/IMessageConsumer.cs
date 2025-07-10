namespace Ambev.DeveloperEvaluation.Domain.Common.Interfaces;

public interface IMessageConsumer<TMessage> where TMessage : IEvent
{
    Task HandleMessageAsync(TMessage message, CancellationToken cancellationToken);
}