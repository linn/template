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

    public class WhenAddingAHub : ContextBase
    {
        private HubResource hubResource;

        [SetUp]
        public void SetUp()
        {
            this.hubResource = new HubResource { HubId = 123, Description = "new" };

            this.FacadeService.Add(Arg.Any<HubResource>()).Returns(
                new CreatedResult<HubResource>(
                    new HubResource
                        {
                            HubId = 123, Description = "new", Links = new[] { new LinkResource("self", "/template/hubs/123") }
                        }));

            this.Response = this.Client.Post(
                "/template/hubs",
                this.hubResource,
                with =>
                    {
                        with.Accept("application/json");
                }).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldReturnLocationHeader()
        {
            this.Response.Headers.Location.Should().NotBeNull();
            this.Response.Headers.Location.Should().Be("/template/hubs/123");
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
            var resources = this.Response.DeserializeBody<HubResource>();
            resources.Should().NotBeNull();

            resources.HubId.Should().Be(123);
            resources.Description.Should().Be("new");
        }
    }
}
