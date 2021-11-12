namespace Linn.Template.Integration.Tests.ConsignmentModuleTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Template.Integration.Tests.Extensions;
    using Linn.Template.Resources.Consignments;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingHubs : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.FacadeService.GetAll().Returns(
                new SuccessResult<IEnumerable<HubResource>>(
                    new[] { new HubResource { HubId = 1 }, new HubResource { HubId = 2 } }));

            this.Response = this.Client.Get(
                "/template/hubs",
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
            this.Response.Content.Headers.ContentType?.ToString().Should().Be("application/json");
        }

        [Test]
        public void ShouldReturnJsonBody()
        {
            var resources = this.Response.DeserializeBody<IEnumerable<HubResource>>()?.ToArray();
            resources.Should().NotBeNull();
            resources.Should().HaveCount(2);

            resources?.First().HubId.Should().Be(1);
            resources.Second().HubId.Should().Be(2);
        }
    }
}
