namespace Linn.Template.Service.ResultHandlers
{
    using System;
    using System.Linq;

    using Linn.Common.Facade.Carter.Handlers;
    using Linn.Template.Resources.Consignments;

    public class HubResourceResultHandler : JsonResultHandler<HubResource>
    {
        public override Func<HubResource, string> GenerateLocation => r => r.Links.FirstOrDefault(l => l.Rel == "self")?.Href;
    }
}
