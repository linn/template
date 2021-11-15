﻿namespace Linn.Template.IoC
{
    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Common.Messaging.RabbitMQ.Configuration;
    using Linn.Common.Messaging.RabbitMQ.Unicast;
    using Linn.Template.Domain.LinnApps.Dispatchers;
    using Linn.Template.Messaging.Dispatchers;

    using Microsoft.Extensions.DependencyInjection;

    public static class MessagingExtensions
    {
        public static IServiceCollection AddMessagingServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IRabbitConfiguration, RabbitConfiguration>()
                .AddScoped<ConnectionBuilder>(c => new ConnectionBuilder(c.GetService<IRabbitConfiguration>()))
                .AddScoped<IConnector>(
                    c => MessagingFactory.CreateConnector(
                        c.GetService<ConnectionBuilder>(),
                        new Connector.RetryInfinitely(100, i => { })))
                .AddScoped<ISender>(
                    a =>
                        {
                            var sender = MessagingFactory.CreateSender();
                            sender.Connector = a.GetService<IConnector>();
                            sender.ExchangeName = "orawin.x";
                            sender.Identity = "Template Message Dispatcher";
                            sender.Init();

                            return sender;
                        })
                .AddScoped<IMessageDispatcher>(d => new MessageDispatcher(d.GetService<ISender>()))
                .AddScoped<IRabbitTerminator, RabbitTerminator>()
                .AddTransient<IMessageSender, MessageSender>();
        }
    }
}