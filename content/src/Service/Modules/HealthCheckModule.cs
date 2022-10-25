namespace Linn.Template.Service.Modules
{
    using Carter;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class HealthCheckModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/healthcheck", async (HttpRequest req, HttpResponse res) => await res.WriteAsync("Ok"));
        }
    }
}
