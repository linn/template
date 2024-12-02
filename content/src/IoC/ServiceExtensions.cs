namespace Linn.Template.IoC
{
    using Linn.Common.Pdf;

    using Microsoft.Extensions.DependencyInjection;
    
    public static class ServiceExtensions
    {
        public static IServiceCollection AddFacade(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ITemplateEngine, RazorTemplateEngine>();
        }
    }
}
