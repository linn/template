namespace Linn.Template.Service.Extensions
{
    using System;
    using System.Linq;

    using Linn.Template.Service.Modules;

    using Microsoft.AspNetCore.Routing;

    public static class EndPointRouteBuilderExtensions
    {
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            var modules = typeof(IModule).Assembly
                .GetTypes()
                .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
                .Select(Activator.CreateInstance)
                .Cast<IModule>();

            foreach (var module in modules)
            {
                module.MapEndpoints(app);
            }
        }
    }
}
