﻿namespace Linn.Template.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Resources;

    public class ThingResourceBuilder : IBuilder<Thing>
    {
        public ThingResource Build(Thing thing, IEnumerable<string> claims)
        {
            var links = this.BuildLinks(thing, claims).ToArray();

            return new ThingResource
            {
                Id = thing.Id,
                Name = thing.Name,
                Code = thing.Code == null ? null : new ThingCodeResource { Code = thing.Code.Code, CodeName = thing.Code.CodeName },
                Details = thing.Details?.Select(
                    d => new ThingDetailResource
                             {
                                 Description = d.Description, DetailId = d.DetailId, ThingId = d.ThingId
                             }),
                Links = links
            };
        }

        public string GetLocation(Thing p)
        {
            return $"/template/things/{p.Id}";
        }

        object IBuilder<Thing>.Build(Thing thing, IEnumerable<string> claims) => this.Build(thing, claims);

        private IEnumerable<LinkResource> BuildLinks(Thing thing, IEnumerable<string> claims)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(thing) };
        }
    }
}
