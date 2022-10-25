namespace Linn.Template.Service.Modules
{
    using System;
    using System.Threading.Tasks;

    using Carter;
    using Carter.Response;

    using Linn.Common.Facade;
    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Resources;
    using Linn.Template.Service.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class ThingModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/template/things", this.GetThings);
            app.MapGet("/template/things/{id:int}", this.GetThingById);
            app.MapPost("/template/things/{id:int}", this.DoNothing);
            app.MapPost("/template/things/send-message", this.SendMessage);
            app.MapPut("/template/things/{id:int}", this.UpdateThing);
            app.MapPost("/template/things", this.CreateThing);
        }

        private async Task GetThings(
            HttpResponse res,
            IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService)
        {
            await res.Negotiate(thingFacadeService.GetAll());
        }

        private async Task SendMessage(HttpRequest req, HttpResponse res, IThingService thingService)
        {
            thingService.SendThingMessage("Test Message");

            await res.Negotiate(new SuccessResult<ProcessResultResource>(new ProcessResultResource(true, "ok")));
        }

        private Task DoNothing(HttpRequest req, HttpResponse res)
        {
            throw new NotImplementedException("This should never be hit");
        }

        private async Task GetThingById(
            HttpRequest req,
            HttpResponse res,
            int id,
            IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService)
        {
            var result = thingFacadeService.GetById(id, req.HttpContext.GetPrivileges());

            await res.Negotiate(result);
        }

        private async Task CreateThing(
            HttpRequest request,
            HttpResponse response,
            ThingResource thingResource,
            IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService)
        {
            var result = thingFacadeService.Add(thingResource);

            await response.Negotiate(result);
        }

        private async Task UpdateThing(
            HttpRequest request,
            HttpResponse response,
            int id,
            ThingResource thingResource,
            IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService)
        {
            var result = thingFacadeService.Update(id, thingResource);

            await response.Negotiate(result);
        }
    }
}
