using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Events;

public class UserRegisteredEvent : IEvent
{
    public const string Exchange = "user-registered-message-exchange";
    public const string Queue = "user-registered-message-queue";

    public UserRegisteredEvent(User user)
    {
        User = user;
    }

    public User User { get; }

    public string ExchangeName => Exchange;
    public string QueueName => Queue;
}