namespace Linn.Template.Integration.Tests
{
    using System;
    using System.Net.Http;

    using Carter;

    using Linn.Template.Service.Modules;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;

    public static class TestClient
    {
        public static HttpClient With(Action<IServiceCollection> serviceConfiguration, params Func<RequestDelegate, RequestDelegate>[] middleWares)
        {
            var server = new TestServer(
                new WebHostBuilder()
                    .ConfigureServices(
                        services =>
                        {
                            services.Apply(serviceConfiguration);
                            services.AddCarter(configurator: c =>
                                c.WithModule<ConsignmentsModule>());
                        })
                    .Configure(
                        app =>
                            {
                                app.UseRouting();
                                app.UseEndpoints(cfg => cfg.MapCarter());
                            }));

            return server.CreateClient();
        }
    }
}
