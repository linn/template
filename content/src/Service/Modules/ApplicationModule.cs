namespace Linn.Template.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Common.Service;
    using Linn.Common.Service.Extensions;
    using Linn.Template.Service.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class ApplicationModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/", this.Redirect);
            app.MapGet("/template", this.GetApp);
            app.MapGet("/template/logged-out", this.GetApp);
        }

        private Task Redirect(HttpRequest req, HttpResponse res)
        {
            res.Redirect("/template");
            return Task.CompletedTask;
        }

        private async Task GetApp(HttpRequest req, HttpResponse res)
        {
            await res.Negotiate(new ViewResponse { ViewName = "Index.cshtml" });
        }
    }
}
