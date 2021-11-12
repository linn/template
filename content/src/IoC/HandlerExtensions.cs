namespace Linn.Template.IoC
{
    using System.Collections.Generic;

    using Linn.Common.Facade.Carter;
    using Linn.Common.Facade.Carter.Handlers;
    using Linn.Template.Resources;
    using Linn.Template.Resources.Consignments;
    using Linn.Template.Service.ResultHandlers;

    using Microsoft.Extensions.DependencyInjection;

    public static class HandlerExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            return services.AddTransient<UniversalResponseNegotiator>()
                .AddTransient<IHandler, HubResourceResultHandler>()
                .AddTransient<IHandler, JsonResultHandler<IEnumerable<HubResource>>>()
                .AddTransient<IHandler, ThingResourceResultHandler>()
                .AddTransient<IHandler, JsonResultHandler<IEnumerable<ThingResource>>>()
                .AddTransient<IHandler, JsonResultHandler<ProcessResultResource>>();
        }
    }
}
