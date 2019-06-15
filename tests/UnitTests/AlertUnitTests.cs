namespace DarkSky.Tests.UnitTests.Models
{
    using DarkSky.Tests.UnitTests.Fixtures;
    using System;
    using Xunit;

    public class AlertUnitTests : IClassFixture<ResponseFixture>
    {
        private readonly ResponseFixture _fixture;

        public AlertUnitTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void AlertModelTimeConversionsTest()
        {
            var forecast = _fixture.NormalResponse; // Thursday, September 20, 2018 4:00:00 AM UTC
            Assert.Equal(forecast.Response.Alerts[0].DateTime, new DateTime(2018, 9, 20, 4, 0, 0, DateTimeKind.Utc));
        }

        [Fact]
        public void AlertModelExpiresTimeConversionsTest()
        {
            var forecast = _fixture.NormalResponse; // Thursday, September 20, 2018 10:00:00 PM UTC
            Assert.Equal(forecast.Response.Alerts[0].ExpiresDateTime, new DateTime(2018, 9, 20, 22, 0, 0, DateTimeKind.Utc));
        }
    }
}