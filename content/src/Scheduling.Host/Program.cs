using Linn.Common.Scheduling;
using Linn.Template.IoC;
using Linn.Template.IoC.Logging.AmazonSQS;

using Scheduling.Host.Jobs;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLog();
        services.AddCredentialsExtensions();
        services.AddSQSExtensions();
        services.AddServices();
        services.AddPersistence();
        services.AddRabbitConfiguration();
        services.AddMessageDispatchers();
        services.AddSingleton<CurrentTime>(() => DateTime.Now);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
