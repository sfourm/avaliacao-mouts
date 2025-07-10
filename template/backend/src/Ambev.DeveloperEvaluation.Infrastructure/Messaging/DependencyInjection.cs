using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Events;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Ambev.DeveloperEvaluation.Infrastructure.Messaging.Bases;
using Ambev.DeveloperEvaluation.Infrastructure.Messaging.Configuration;
using Ambev.DeveloperEvaluation.Infrastructure.Messaging.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Ambev.DeveloperEvaluation.Infrastructure.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var masstransit = configuration
            .GetRequiredSection(nameof(MessagingConfiguration))
            .Get<MessagingConfiguration>();

        services.AddMassTransit(x =>
        {
            ConfigureConsumers(x);

            x.UsingRabbitMq((context, cfg) =>
            {
                ConfigureRabbitMqBroker(cfg, masstransit!.Broker);
                ConfigureRabbitMqEndpoints(context, cfg, masstransit.Consumer);
            });
        });

        services.AddScoped(typeof(IMessageProducer<>), typeof(MessageProducer<>));

        return services;
    }

    private static void ConfigureConsumers(IRegistrationConfigurator configurator)
    {
        configurator.AddConsumer<UserRegisteredConsumer>();
    }

    private static void ConfigureRabbitMqEndpoints(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator cfg,
        Consumer consumerConfig)
    {
        ConfigureEndpoint<UserRegisteredEvent, UserRegisteredConsumer>(cfg, context, consumerConfig);
    }

    private static void ConfigureRabbitMqBroker(IRabbitMqBusFactoryConfigurator cfg, BrokerConfiguration broker)
    {
        cfg.Host(broker.Host, broker.Port, broker.VirtualHost, c =>
        {
            c.Username(broker.Username);
            c.Password(broker.Password);
        });
    }

    private static void ConfigureEndpoint<TMessage, TConsumer>(
        IRabbitMqBusFactoryConfigurator cfg,
        IBusRegistrationContext context,
        Consumer consumerConfig)
        where TMessage : class, IEvent
        where TConsumer : class, IConsumer<TMessage>
    {
        var messageInstance = Activator.CreateInstance<TMessage>();

        cfg.Message<TMessage>(m => { m.SetEntityName(messageInstance.ExchangeName); });

        cfg.Publish<TMessage>(p => { p.ExchangeType = ExchangeType.Topic; });

        cfg.ReceiveEndpoint(messageInstance.QueueName, e =>
        {
            e.ConfigureConsumeTopology = false;
            e.PrefetchCount = consumerConfig.PrefetchCount;
            e.ConcurrentMessageLimit = consumerConfig.ConcurrencyLimit;
            e.SetQuorumQueue();

            e.UseDelayedRedelivery(r =>
            {
                r.Incremental(
                    consumerConfig.Retries,
                    TimeSpan.FromSeconds(consumerConfig.InitialIntervalInSeconds),
                    TimeSpan.FromSeconds(consumerConfig.SleepDurationInSeconds)
                );
            });

            e.Bind(messageInstance.ExchangeName, configurator =>
            {
                configurator.Durable = true;
                configurator.ExchangeType = ExchangeType.Topic;
            });

            e.DiscardSkippedMessages();
            e.DiscardFaultedMessages();
            e.Consumer<TConsumer>(context);
        });
    }
}