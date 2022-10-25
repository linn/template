namespace Linn.Template.Integration.Tests.ThingModuleTests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http.Json;

    using FluentAssertions;

    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Integration.Tests.Extensions;
    using Linn.Template.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingAThing : ContextBase
    {
        private Thing thing;

        private ThingResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new ThingResource
                                {
                                    Id = 123, 
                                    Name = "new", 
                                    Code = new ThingCodeResource(), 
                                    Details = new List<ThingDetailResource>()
                                };
            this.thing = new Thing { Id = 123, Name = "new" };

            this.ThingService.CreateThing(Arg.Any<Thing>()).Returns(this.thing);

            this.Response = this.Client.PostAsJsonAsync(
                "/template/things",
                this.resource).Result;
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
            this.Response.Headers.Location.Should().Be("/template/things/123");
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
            var resources = this.Response.DeserializeBody<ThingResource>();
            resources.Should().NotBeNull();

            resources.Id.Should().Be(123);
            resources.Name.Should().Be("new");
        }
    }
}
