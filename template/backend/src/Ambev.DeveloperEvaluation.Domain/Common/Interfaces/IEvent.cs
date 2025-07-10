namespace Ambev.DeveloperEvaluation.Domain.Common.Interfaces;

public interface IEvent
{
    string ExchangeName { get; }
    string QueueName { get; }
}