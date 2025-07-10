namespace Ambev.DeveloperEvaluation.Infrastructure.Messaging.Configuration;

public class MessagingConfiguration
{
    public Consumer Consumer { get; set; } = null!;
    public BrokerConfiguration Broker { get; set; } = null!;
}

public class Consumer
{
    public int ConcurrencyLimit { get; set; }
    public int PrefetchCount { get; set; }
    public int Retries { get; set; } = 5;
    public int InitialIntervalInSeconds { get; set; } = 1;
    public int SleepDurationInSeconds { get; set; } = 1;
}

public class BrokerConfiguration
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Host { get; init; } = string.Empty;
    public ushort Port { get; init; }
    public string VirtualHost { get; init; } = string.Empty;
}