﻿namespace Linn.Template.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Common.Facade;
    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Resources;
    using Linn.Template.Service.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class ThingModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("template/things", this.GetThings);
            endpoints.MapGet("/template/things/{id:int}", this.GetThingById);
            endpoints.MapPut("/template/things/{id:int}", this.PutThing);
            endpoints.MapPost("/template/things", this.PostThing);
        }

        private async Task GetThings(
            HttpResponse res,
            IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService)
        {
            await res.Negotiate(thingFacadeService.GetAll());
        }

        private async Task GetThingById(
            int id,
            HttpRequest req,
            HttpResponse res,
            IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService)
        {
            await res.Negotiate(thingFacadeService.GetById(id));
        }

        private async Task PutThing(
            int id,
            HttpRequest req,
            HttpResponse res,
            ThingResource resource,
            IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService)
        {
            await res.Negotiate(thingFacadeService.Update(id, resource));
        }

        private async Task PostThing(
            HttpRequest req,
            HttpResponse res,
            ThingResource resource,
            IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService)
        {
            await res.Negotiate(thingFacadeService.Add(resource));
        }
    }
}
