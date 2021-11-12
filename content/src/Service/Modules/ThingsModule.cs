namespace Linn.Template.Service.Modules
{
    using System;
    using System.Threading.Tasks;

    using Carter;
    using Carter.ModelBinding;
    using Carter.Request;
    using Carter.Response;

    using Linn.Common.Facade;
    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Resources;
    using Linn.Template.Service.Extensions;

    using Microsoft.AspNetCore.Http;

    public class ThingsModule : CarterModule
    {
        private readonly IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService;

        private readonly IThingService thingService;

        public ThingsModule(IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService, IThingService thingService)
        {
            this.thingFacadeService = thingFacadeService;
            this.thingService = thingService;
            this.Get("/template/things", this.GetThings);
            this.Get("/template/things/{id:int}", this.GetThingById);
            this.Post("/template/things/{id:int}", this.DoNothing);
            this.Post("/template/things/send-message", this.SendMessage);
            this.Post("/template/things", this.CreateThing);
        }

        private async Task SendMessage(HttpRequest req, HttpResponse res)
        {
            this.thingService.SendThingMessage("Test Message");

            await res.Negotiate(new SuccessResult<ProcessResultResource>(new ProcessResultResource(true, "ok")));
        }

        private Task DoNothing(HttpRequest req, HttpResponse res)
        {
            throw new NotImplementedException("This should never be hit");
        }

        private async Task GetThings(HttpRequest req, HttpResponse res)
        {
            await res.Negotiate(this.thingFacadeService.GetAll());
        }

        private async Task GetThingById(HttpRequest req, HttpResponse res)
        {
            var thingId = req.RouteValues.As<int>("id");

            var result = this.thingFacadeService.GetById(thingId, req.HttpContext.GetPrivileges());

            await res.Negotiate(result);
        }

        private async Task CreateThing(HttpRequest request, HttpResponse response)
        {
            var resource = await request.Bind<ThingResource>();
            var result = this.thingFacadeService.Add(resource);

            await response.Negotiate(result);
        }
    }
}
