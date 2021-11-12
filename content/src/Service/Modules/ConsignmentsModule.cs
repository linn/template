namespace Linn.Template.Service.Modules
{
    using System.Threading.Tasks;

    using Carter;
    using Carter.ModelBinding;
    using Carter.Request;
    using Carter.Response;

    using Linn.Common.Facade;
    using Linn.Common.Logging;
    using Linn.Template.Domain.LinnApps.Consignments;
    using Linn.Template.Resources.Consignments;

    using Microsoft.AspNetCore.Http;

    public class ConsignmentsModule : CarterModule
    {
        private readonly IFacadeResourceService<Hub, int, HubResource, HubResource> hubFacadeService;

        private readonly ILog log;

        public ConsignmentsModule(IFacadeResourceService<Hub, int, HubResource, HubResource> hubFacadeService, ILog log)
        {
            this.hubFacadeService = hubFacadeService;
            this.log = log;
            this.Get("/template/hubs", this.GetHubs);
            this.Get("/template/hubs/{id:int}", this.GetHubById);
            this.Post("/template/hubs", this.AddHub);
            this.Put("/template/hubs/{id:int}", this.UpdateHub);
        }

        private async Task AddHub(HttpRequest request, HttpResponse response)
        {
            var resource = await request.Bind<HubResource>();
            var result = this.hubFacadeService.Add(resource); 
            
            await response.Negotiate(result);
        }

        private async Task UpdateHub(HttpRequest request, HttpResponse response)
        {
            var id = request.RouteValues.As<int>("id");
            var resource = await request.Bind<HubResource>();
            var result = this.hubFacadeService.Update(id, resource);
        
            await response.Negotiate(result);
        }

        private async Task GetHubs(HttpRequest req, HttpResponse res)
        {
            this.log.Info("It's ok. we're just getting some hubs");
            await res.Negotiate(this.hubFacadeService.GetAll());
        }

        private async Task GetHubById(HttpRequest req, HttpResponse res)
        {
            var hubId = req.RouteValues.As<int>("id");

            var result = this.hubFacadeService.GetById(hubId);

            await res.Negotiate(result);
        }
    }
}
