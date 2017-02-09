namespace DarkSky.UnitTests.Services
{
	using System;
	using System.Threading.Tasks;
	using DarkSky.Services;
	using NUnit.Framework;

	public class DarkSkyServiceUnitTests
	{
		readonly MockClientFixture _fixture = new MockClientFixture();
		readonly double _latitude = 29.4264; // 42.915;
		readonly double _longitude = -98.5105; // -78.741;
		[DatapointSource]
		readonly string[] _values = new string[] { null, string.Empty, " ", "  ", "\t" };

		[Test]
		public void ConstructorWithNonEmptyApiKey()
		{
			var darkSkyService = new DarkSkyService("fakekey");
			Assert.NotNull(darkSkyService);
		}

		[Theory]
		public void ExceptionThrownForMissingApiKey(string value)
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var darkSkyService = new DarkSkyService(value);
				var result = darkSkyService.GetForecast(0, 0);
			});
		}

		[Test]
		public async Task GetForecastWithMockData()
		{
			var darkSkyService = new DarkSkyService("fakekey", _fixture.MockClient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			Assert.NotNull(forecast);

			// Check Response (basic deserialization check)
			Assert.NotNull(forecast.Response);
			Assert.AreEqual(forecast.Response.Latitude, _latitude);
			Assert.AreEqual(forecast.Response.Longitude, _longitude);
			Assert.IsNotEmpty(forecast.Response.Alerts);
			Assert.NotNull(forecast.Response.Currently);
			Assert.NotNull(forecast.Response.Daily);
			Assert.IsNotEmpty(forecast.Response.Daily.Data);
			Assert.NotNull(forecast.Response.Flags);
			Assert.NotNull(forecast.Response.Hourly);
			Assert.IsNotEmpty(forecast.Response.Hourly.Data);
			Assert.NotNull(forecast.Response.Minutely);
			Assert.IsNotEmpty(forecast.Response.Minutely.Data);
			Assert.NotNull(forecast.Response.Timezone);

			// Check Headers (match pre-defined values)
			Assert.NotNull(forecast.Headers);
			Assert.AreEqual(forecast.Headers.ApiCalls.Value, _fixture.ApiCalls);
			Assert.AreEqual(forecast.Headers.CacheControl.MaxAge.Value.TotalMinutes, _fixture.CacheMinutes);
			Assert.AreEqual(forecast.Headers.ResponseTime, _fixture.ResponseTime);
		}
	}
}