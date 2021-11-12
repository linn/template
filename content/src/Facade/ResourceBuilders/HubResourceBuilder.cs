namespace Linn.Template.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Template.Domain.LinnApps.Consignments;
    using Linn.Template.Resources.Consignments;

    public class HubResourceBuilder : IBuilder<Hub>
    {
        public HubResource Build(Hub hub, IEnumerable<string> claims)
        {
            return new HubResource
            {
                HubId = hub.HubId,
                AddressId = hub.AddressId,
                CarrierCode = hub.CarrierCode, 
                CustomStamp = hub.CustomStamp,
                Description = hub.Description,
                EcHub = hub.EcHub,
                OrgId = hub.OrgId,
                Links = this.BuildLinks(hub).ToArray()
            };
        }

        public string GetLocation(Hub p)
        {
            return $"/template/hubs/{p.HubId}";
        }

        object IBuilder<Hub>.Build(Hub hub, IEnumerable<string> claims) => this.Build(hub, claims);

        private IEnumerable<LinkResource> BuildLinks(Hub hub)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(hub) };
        }
    }
}
