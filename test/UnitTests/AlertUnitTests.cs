namespace DarkSky.UnitTests.Models
{
	using System;
	using DarkSky.UnitTests.Fixtures;
	using Xunit;

	public class AlertUnitTests : IClassFixture<ResponseFixture>
	{
		ResponseFixture _fixture;

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
	}
}