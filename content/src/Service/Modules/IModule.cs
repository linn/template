namespace Linn.Template.Service.Modules
{
    using Microsoft.AspNetCore.Routing;

    public interface IModule
    {
        void MapEndpoints(IEndpointRouteBuilder endpoints);
    }
}
