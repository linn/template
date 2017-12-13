﻿namespace Linn.Template.Messaging.Host
{
    using Autofac;

    using Linn.Common.Messaging.RabbitMQ.Autofac;
    using Linn.Template.IoC;

    public static class Configuration
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AmazonCredentialsModule>();
            builder.RegisterModule<AmazonSqsModule>();
            builder.RegisterModule<LoggingModule>();
            //builder.RegisterModule<MessagingModule>();
            //builder.RegisterModule<PersistenceModule>();
            //builder.RegisterModule<ServiceModule>();
            builder.RegisterReceiver("template.q", "template.dlx");

            builder.RegisterType<Listener>().AsSelf();

            return builder.Build();
        }
    }
}
