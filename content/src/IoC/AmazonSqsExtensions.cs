namespace Linn.Template.IoC
{
    using Amazon;
    using Amazon.Runtime;
    using Amazon.SQS;


    using Microsoft.Extensions.DependencyInjection;

    public static class AmazonSqsExtensions
    {
        public static IServiceCollection AddSqsExtensions(this IServiceCollection services)
        {
            services.AddSingleton<AmazonSQSClient>(sp =>
                {
                    var creds = sp.GetRequiredService<AWSCredentials>();
                    var region = sp.GetRequiredService<RegionEndpoint>();
                    return new AmazonSQSClient(creds, region);
                });

            return services;
        }
    }
}
