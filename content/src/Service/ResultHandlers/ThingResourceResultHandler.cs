namespace Linn.Template.Service.ResultHandlers
{
    using System;
    using System.Linq;

    using Linn.Common.Service.Core.Handlers;
    using Linn.Template.Resources;

    public class ThingResourceResultHandler : JsonResultHandler<ThingResource>
    {
        public override Func<ThingResource, string> GenerateLocation => r => r.Links.FirstOrDefault(l => l.Rel == "self")?.Href;
    }
}
