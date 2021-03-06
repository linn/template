﻿namespace Linn.Template.IoC
{
    using Amazon.SQS;
    using Autofac;

    using Linn.Common.Logging;
    using Linn.Common.Logging.AmazonSqs;

    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Uncomment these lines

//#if DEBUG
//            builder.RegisterType<Linn.Common.Logging.ConsoleLog>().As<ILog>().SingleInstance();
//#else
//            builder.Register(c => new AmazonSqsLog(c.Resolve<IAmazonSQS>(), LoggingConfiguration.Environment, LoggingConfiguration.MaxInnerExceptionDepth, LoggingConfiguration.AmazonSqsQueueUri))
//                .As<ILog>()
//                .SingleInstance();
//#endif
        }
    }
}