﻿namespace Linn.Template.Integration.Tests.ConsignmentModuleTests
{
    using System.Net.Http;

    using Linn.Common.Facade;
    using Linn.Common.Logging;
    using Linn.Common.Persistence;
    using Linn.Template.Domain.LinnApps.Consignments;
    using Linn.Template.IoC;
    using Linn.Template.Resources.Consignments;
    using Linn.Template.Service.Modules;

    using Microsoft.Extensions.DependencyInjection;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected HttpClient Client { get; set; }

        protected HttpResponseMessage Response { get; set; }

        protected ITransactionManager TransactionManager { get; set; }

        protected IFacadeResourceService<Hub, int, HubResource, HubResource> FacadeService { get; private set; }

        protected ILog Log { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.FacadeService = Substitute.For<IFacadeResourceService<Hub, int, HubResource, HubResource>>();
            this.Log = Substitute.For<ILog>();

            this.Client = TestClient.With<ConsignmentsModule>(
                services =>
                    {
                        services.AddSingleton(this.TransactionManager);
                        services.AddSingleton(this.FacadeService);
                        services.AddSingleton(this.Log);
                        services.AddHandlers();
                    },
                FakeAuthMiddleware.EmployeeMiddleware);
        }
    }
}
