namespace Linn.Template.IoC
{
    using Linn.Common.Logging;
    using Linn.Common.Messaging.RabbitMQ.Configuration;
    using Linn.Common.Messaging.RabbitMQ.Dispatchers;
    using Linn.Common.Messaging.RabbitMQ.Handlers;
    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Messaging.Handlers;
    using Linn.Template.Messaging.Messages;
    using Linn.Template.Resources;

    using Microsoft.Extensions.DependencyInjection;
    using RabbitMQ.Client.Events;

    public static class MessagingExtensions
    {
        public static IServiceCollection AddRabbitConfiguration(this IServiceCollection services)
        {
            // all the routing keys the Listener cares about need to be registered here:
            var routingKeys = new[] { ThingMessage.RoutingKey };

            return services.AddSingleton<ChannelConfiguration>(d => new ChannelConfiguration("template", routingKeys))
                .AddScoped(d => new EventingBasicConsumer(d.GetService<ChannelConfiguration>()?.ConsumerChannel));
        }

        public static IServiceCollection AddMessageHandlers(this IServiceCollection services)
        {
            // register handlers for different message types
            return services.AddScoped<Handler<ThingMessage>, ThingMessageHandler>();
        }

        public static IServiceCollection AddMessageDispatchers(this IServiceCollection services)
        {
            // register dispatchers for different message types:
            return services.AddTransient<IMessageDispatcher<Thing>>(
            x => new RabbitMessageDispatcher<Thing>(
                x.GetService<ChannelConfiguration>(), x.GetService<ILog>(), ThingMessage.RoutingKey));
        }
    }
}
