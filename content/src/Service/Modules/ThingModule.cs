namespace Linn.Template.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Resources;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class ThingModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("template/things", GetThings);
            endpoints.MapGet("/template/things/{id:int}", GetThingById);
        }

        private static dynamic GetThings(
            IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService)
        {
            return thingFacadeService.GetAll();
        }

        private static dynamic GetThingById(
            int id,
            HttpRequest req,
            HttpResponse res,
            IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService)
        {
            return thingFacadeService.GetById(id);
        }
    }
}
