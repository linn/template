namespace Linn.Template.IoC
{
    using Linn.Common.Resources;
    using Linn.Common.Service.Handlers;

    using Microsoft.Extensions.DependencyInjection;

    public static class HandlerExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IHandler, JsonResultHandler<ProcessResultResource>>();
        }
    }
}
