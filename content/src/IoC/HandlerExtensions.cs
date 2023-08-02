namespace Linn.Template.IoC
{
    using System.Collections.Generic;

    using Linn.Common.Facade.Carter;
    using Linn.Common.Facade.Carter.Handlers;
    using Linn.Template.Resources;

    using Microsoft.Extensions.DependencyInjection;

    public static class HandlerExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            return services.AddTransient<UniversalResponseNegotiator>()
                .AddTransient<IHandler, JsonResultHandler<ThingResource>>()
                .AddTransient<IHandler, JsonResultHandler<IEnumerable<ThingResource>>>()
                .AddTransient<IHandler, JsonResultHandler<ProcessResultResource>>()
                .AddTransient<IHandler, CsvResultHandler<IEnumerable<ThingResource>>>()
                .AddTransient<IHandler, CsvResultHandler<ThingResource>>();
        }
    }
}
