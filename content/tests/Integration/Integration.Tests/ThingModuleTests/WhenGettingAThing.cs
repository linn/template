namespace Linn.Template.Integration.Tests.ThingModuleTests
{
    using System.Net;

    using FluentAssertions;

    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Integration.Tests.Extensions;
    using Linn.Template.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAThing : ContextBase
    {
        private int thingId;

        private Thing thing;

        [SetUp]
        public void SetUp()
        {
            this.thingId = 1;
            this.thing = new Thing { Id = this.thingId };

            this.ThingRepository.FindById(this.thingId).Returns(this.thing);

            this.Response = this.Client.Get(
                $"/template/things/{this.thingId}",
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
            var resource = this.Response.DeserializeBody<ThingResource>();
            resource.Should().NotBeNull();

            resource.Id.Should().Be(this.thingId);
        }
    }
}
