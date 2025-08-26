﻿namespace Linn.Template.IoC.Logging.AmazonSQS
{
    using Amazon;
    using Amazon.Runtime;
    using Amazon.SQS;

    using Microsoft.Extensions.DependencyInjection;

    public static class AmazonSqsExtensions
    {
        public static IServiceCollection AddSQSExtensions(this IServiceCollection services)
        {
            return services.AddSingleton<IAmazonSQS>(
                s => new AmazonSQSClient(s.GetService<AWSCredentials>(), s.GetService<RegionEndpoint>()));
        }
    }
}