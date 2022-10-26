﻿namespace Linn.Template.Integration.Tests.ThingModuleTests
{
    using System.Net.Http;

    using Linn.Common.Facade;
    using Linn.Common.Logging;
    using Linn.Common.Persistence;
    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Facade.ResourceBuilders;
    using Linn.Template.Facade.Services;
    using Linn.Template.IoC;
    using Linn.Template.Resources;
    using Linn.Template.Service.Modules;

    using Microsoft.Extensions.DependencyInjection;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected HttpClient Client { get; set; }

        protected HttpResponseMessage Response { get; set; }

        protected ITransactionManager TransactionManager { get; set; }

        protected IFacadeResourceService<Thing, int, ThingResource, ThingResource> FacadeService { get; private set; }

        protected ILog Log { get; private set; }

        protected IThingService ThingService { get; private set; }

        protected IRepository<Thing, int> ThingRepository { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.ThingRepository = Substitute.For<IRepository<Thing, int>>();
            this.ThingService = Substitute.For<IThingService>();

            this.FacadeService = new ThingFacadeService(
                this.ThingRepository,
                this.TransactionManager,
                new ThingResourceBuilder(),
                this.ThingService);
            this.Log = Substitute.For<ILog>();

            this.Client = TestClient.With<ThingModule>(
                services =>
                    {
                        services.AddSingleton(this.TransactionManager);
                        services.AddSingleton(this.FacadeService);
                        services.AddSingleton(this.Log);
                        services.AddSingleton(this.ThingService);
                        services.AddHandlers();
                        services.AddRouting();
                    },
                FakeAuthMiddleware.EmployeeMiddleware);
        }
    }
}
