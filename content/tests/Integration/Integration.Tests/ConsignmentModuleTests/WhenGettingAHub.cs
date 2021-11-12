namespace Linn.Template.Integration.Tests.ConsignmentModuleTests
{
    using System.Net;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Template.Integration.Tests.Extensions;
    using Linn.Template.Resources.Consignments;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAHub : ContextBase
    {
        private int hubId;

        private HubResource hub;

        [SetUp]
        public void SetUp()
        {
            this.hubId = 1;
            this.hub = new HubResource { HubId = this.hubId };

            this.FacadeService.GetById(this.hubId).Returns(new SuccessResult<HubResource>(this.hub));

            this.Response = this.Client.Get(
                $"/template/hubs/{this.hubId}",
                with =>
                    {
                        with.Accept("application/json");
            }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnJsonContentType()
        {
            this.Response.Content.Headers.ContentType.Should().NotBeNull();
            this.Response.Content.Headers.ContentType.ToString().Should().Be("application/json");
        }

        [Test]
        public void ShouldReturnJsonBody()
        {
            var resource = this.Response.DeserializeBody<HubResource>();
            resource.Should().NotBeNull();

            resource.HubId.Should().Be(this.hubId);
        }
    }
}
