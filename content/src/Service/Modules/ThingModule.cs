namespace Linn.Template.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Resources;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System.Threading.Tasks;

    using Linn.Template.Service.Extensions;

    public class ThingModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("template/things", GetThings);
            endpoints.MapGet("/template/things/{id:int}", GetThingById);
        }

        private async Task GetThings(
            HttpResponse res,
            IFacadeResourceService<Thing, int, ThingResource, ThingResource> thingFacadeService)
        {
            await res.Negotiate(thingFacadeService.GetAll());
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
