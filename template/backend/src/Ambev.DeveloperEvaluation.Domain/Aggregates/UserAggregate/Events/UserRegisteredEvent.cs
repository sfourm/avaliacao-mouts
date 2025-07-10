namespace Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Events;

public sealed class UserRegisteredEvent : IEvent
{
    private const string Exchange = "user-registered-message-exchange";
    private const string Queue = "user-registered-message-queue";
    
    public Guid Id { get; init; }
    
    public string ExchangeName => Exchange;
    public string QueueName => Queue;
}