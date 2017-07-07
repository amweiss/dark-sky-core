namespace DarkSky.UnitTests.Services
{
	using System;
	using System.Threading.Tasks;
	using DarkSky.Services;
	using Xunit;

	public class DarkSkyServiceUnitTests : IClassFixture<MockClientFixture>
	{
		readonly MockClientFixture _fixture;
		readonly double _latitude = 29.4264; // 42.915;
		readonly double _longitude = -98.5105; // -78.741;

		public DarkSkyServiceUnitTests(MockClientFixture fixture)
		{
			_fixture = fixture;
		}

		[Fact]
		public void ConstructorWithNonEmptyApiKey()
		{
			var darkSkyService = new DarkSkyService("fakekey");
			Assert.NotNull(darkSkyService);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("	")]
		[InlineData("  ")]
		public void ExceptionThrownForMissingApiKey(string value)
		{
			Assert.ThrowsAny<ArgumentException>(() =>
			{
				var darkSkyService = new DarkSkyService(value);
				var result = darkSkyService.GetForecast(0, 0);
			});
		}

		[Fact]
		public async Task GetForecastWithMockData()
		{
			_fixture.AddApiCallsHeader = true;
			_fixture.ApiCallsResponseText = _fixture.ApiCalls.ToString();
			_fixture.AddResponseTimeHeader = true;
			var darkSkyService = new DarkSkyService("fakekey", _fixture.MockClient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			Assert.NotNull(forecast);

			// Check Response (basic deserialization check)
			Assert.NotNull(forecast.Response);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
			Assert.NotEmpty(forecast.Response.Alerts);
			Assert.NotNull(forecast.Response.Currently);
			Assert.NotNull(forecast.Response.Daily);
			Assert.NotEmpty(forecast.Response.Daily.Data);
			Assert.NotNull(forecast.Response.Flags);
			Assert.NotNull(forecast.Response.Hourly);
			Assert.NotEmpty(forecast.Response.Hourly.Data);
			Assert.NotNull(forecast.Response.Minutely);
			Assert.NotEmpty(forecast.Response.Minutely.Data);
			Assert.NotNull(forecast.Response.Timezone);

			// Check Headers (match pre-defined values)
			Assert.NotNull(forecast.Headers);
			Assert.Equal(forecast.Headers.ApiCalls.Value, _fixture.ApiCalls);
			Assert.Equal(forecast.Headers.CacheControl.MaxAge.Value.TotalMinutes, _fixture.CacheMinutes);
			Assert.Equal(forecast.Headers.ResponseTime, _fixture.ResponseTime);
		}

		[Fact]
		public async Task ApiCallsHeaderBadValueTest()
		{
			_fixture.ApiCallsResponseText = "cows";
			_fixture.AddApiCallsHeader = true;
			_fixture.AddResponseTimeHeader = true;
			var darkSkyService = new DarkSkyService("fakekey", _fixture.MockClient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			// Check Headers (match pre-defined values)
			Assert.NotNull(forecast.Headers);
			Assert.Null(forecast.Headers.ApiCalls);
		}

		[Fact]
		public async Task ApiCallsHeaderNullValueTest()
		{
			_fixture.ApiCallsResponseText = null;
			_fixture.AddApiCallsHeader = true;
			_fixture.AddResponseTimeHeader = true;
			var darkSkyService = new DarkSkyService("fakekey", _fixture.MockClient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			// Check Headers (match pre-defined values)
			Assert.NotNull(forecast.Headers);
			Assert.Null(forecast.Headers.ApiCalls);
		}

		[Fact]
		public async Task ApiCallsHeaderMissingTest()
		{
			_fixture.ApiCallsResponseText = null;
			_fixture.AddApiCallsHeader = false;
			_fixture.AddResponseTimeHeader = true;
			var darkSkyService = new DarkSkyService("fakekey", _fixture.MockClient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			// Check Headers (match pre-defined values)
			Assert.NotNull(forecast.Headers);
			Assert.Null(forecast.Headers.ApiCalls);
		}

		[Fact]
		public async Task ResponseTimeHeaderMissingTest()
		{
			_fixture.AddApiCallsHeader = true;
			_fixture.ApiCallsResponseText = _fixture.ApiCalls.ToString();
			_fixture.AddResponseTimeHeader = false;
			var darkSkyService = new DarkSkyService("fakekey", _fixture.MockClient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			// Check Headers (match pre-defined values)
			Assert.NotNull(forecast.Headers);
			Assert.Null(forecast.Headers.ResponseTime);
		}
	}
}