namespace Linn.Template.Integration.Tests.ConsignmentModuleTests
{
    using System.Net;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Template.Integration.Tests.Extensions;
    using Linn.Template.Resources.Consignments;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingAHub : ContextBase
    {
        private HubResource resource;

        private int hubId;

        [SetUp]
        public void SetUp()
        {
            this.hubId = 123;

            this.resource = new HubResource { HubId = this.hubId, Description = "new description" };

            this.FacadeService.Update(this.hubId, Arg.Is<HubResource>(a => a.HubId == this.hubId))
                .Returns(
                    new SuccessResult<HubResource>(
                        new HubResource
                            {
                                HubId = this.hubId,
                                Description = this.resource.Description,
                                Links = new[] { new LinkResource("self", $"/template/hubs/{this.hubId}") }
                            }));

            this.Response = this.Client.Put(
                $"/template/hubs/{this.hubId}",
                this.resource,
                with =>
                    {
                        with.Accept("application/json");
                }).Result;
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallUpdate()
        {
            this.FacadeService.Received().Update(this.hubId, Arg.Is<HubResource>(a => a.HubId == this.hubId));
        }

        [Test]
        public void ShouldReturnJsonContentType()
        {
            this.Response.Content.Headers.ContentType.Should().NotBeNull();
            this.Response.Content.Headers.ContentType?.ToString().Should().Be("application/json");
        }

        [Test]
        public void ShouldReturnJsonBody()
        {
            var resultResource = this.Response.DeserializeBody<HubResource>();
            resultResource.Should().NotBeNull();

            resultResource.Description.Should().Be("new description");
        }
    }
}
