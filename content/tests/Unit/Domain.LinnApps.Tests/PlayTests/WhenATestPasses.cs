namespace Linn.Template.Domain.LinnApps.Tests.PlayTests
{
    using FluentAssertions;

    using Linn.Template.Domain.LinnApps.Consignments;

    using NUnit.Framework;

    public class WhenATestPasses
    {
        private Hub hub;

        [SetUp]
        public void SetUp()
        {
            this.hub = new Hub { HubId = 1, Description = "new" };
        }

        [Test]
        public void ShouldBeAHub()
        {
            this.hub.HubId.Should().Be(1);
            this.hub.Description.Should().Be("new");
        }
    }
}
