namespace Linn.Template.IoC
{
    using System.Collections.Generic;

    using Linn.Common.Service.Core;
    using Linn.Common.Service.Core.Handlers;
    using Linn.Template.Resources;

    using Microsoft.Extensions.DependencyInjection;

    public static class HandlerExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            return services
                .AddScoped<IHandler, JsonResultHandler<ProcessResultResource>>();
        }
    }
}
