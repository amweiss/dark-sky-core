#region

using System;
using DarkSky.Tests.UnitTests.Fixtures;
using Xunit;

#endregion

namespace DarkSky.Tests.UnitTests.Models
{
    public class AlertUnitTests : IClassFixture<ResponseFixture>
    {
        public AlertUnitTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ResponseFixture _fixture;

        [Fact]
        public void AlertModelDescriptionTest()
        {
            var forecast = _fixture.AlertResponse;
            Assert.Equal(
                "of persistent and heavy rain is expected to develop across Wales and northwest England. Spray and flooding on roads probably making journey times longer. Bus and train services probably affected with journey times taking longer. Some interruption to power supplies and other services likely. Flooding of a few homes and business is likely\n",
                forecast.Response.Alerts[0].Description);
        }

        [Fact]
        public void AlertModelExpiresTimeConversionsTest()
        {
            var forecast = _fixture.AlertResponse;
            Assert.Equal(new DateTime(2018, 9, 20, 22, 0, 0, DateTimeKind.Utc),
                forecast.Response.Alerts[0].ExpiresDateTime);
        }

        [Fact]
        public void AlertModelSeverityTest()
        {
            var forecast = _fixture.AlertResponse;
            Assert.Equal("watch", forecast.Response.Alerts[0].Severity);
        }

        [Fact]
        public void AlertModelTimeConversionsTest()
        {
            var forecast = _fixture.AlertResponse;
            Assert.Equal(new DateTime(2018, 9, 20, 4, 0, 0, DateTimeKind.Utc), forecast.Response.Alerts[0].DateTime);
        }

        [Fact]
        public void AlertModelTitleTest()
        {
            var forecast = _fixture.AlertResponse;
            Assert.Equal("Yellow Rain Warning For United Kingdom - Yorkshire And The Humber",
                forecast.Response.Alerts[0].Title);
        }

        [Fact]
        public void AlertModelUriTest()
        {
            var forecast = _fixture.AlertResponse;
            Assert.Equal(new Uri("http://meteoalarm.eu/en_UK/0/0/UK008.html"), forecast.Response.Alerts[0].Uri);
        }
    }
}