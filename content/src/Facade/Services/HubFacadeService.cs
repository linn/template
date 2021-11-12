namespace Linn.Template.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Template.Domain.LinnApps.Consignments;
    using Linn.Template.Resources.Consignments;

    public class HubFacadeService : FacadeResourceService<Hub, int, HubResource, HubResource>
    {
        public HubFacadeService(IRepository<Hub, int> repository, ITransactionManager transactionManager, IBuilder<Hub> resourceBuilder)
            : base(repository, transactionManager, resourceBuilder)
        {
        }

        protected override Hub CreateFromResource(HubResource resource)
        {
            return new Hub
                       {
                           Description = resource.Description,
                           AddressId = resource.AddressId,
                           CarrierCode = resource.CarrierCode,
                           CustomStamp = resource.CustomStamp,
                           EcHub = resource.EcHub,
                           OrgId = resource.OrgId
                       };
        }

        protected override void UpdateFromResource(Hub entity, HubResource updateResource)
        {
            entity.Description = updateResource.Description;
            entity.AddressId = updateResource.AddressId;
            entity.CarrierCode = updateResource.CarrierCode;
            entity.CustomStamp = updateResource.CustomStamp;
            entity.EcHub = updateResource.EcHub;
            entity.OrgId = updateResource.OrgId;
        }

        protected override Expression<Func<Hub, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
