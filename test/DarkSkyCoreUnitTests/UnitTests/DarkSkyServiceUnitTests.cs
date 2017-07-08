namespace DarkSky.UnitTests.Services
{
	using System;
	using System.IO;
	using System.Net.Http;
	using System.Threading.Tasks;
	using DarkSky.Services;
	using Moq;
	using Xunit;

	public class DarkSkyServiceUnitTests
	{
		readonly double _latitude = 29.4264; // 42.915;
		readonly double _longitude = -98.5105; // -78.741;

		readonly HttpResponseMessage _mockHttpResponse = new HttpResponseMessage
		{
			Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/BexarTX.json")),
		};

		readonly HttpResponseMessage _mockHttpResponseMissingData = new HttpResponseMessage
		{
			Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/BuffaloNY_MissingBlocks.json")),
		};

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
			var mockCLient = new Mock<IHttpClient>();
			mockCLient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(_mockHttpResponse));

			var darkSkyService = new DarkSkyService("fakekey", mockCLient.Object);
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
			Assert.NotNull(forecast.Response.TimeZone);
		}

		[Fact]
		public async Task DailyBlockEmptyTest()
		{
			var mockCLient = new Mock<IHttpClient>();
			mockCLient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(_mockHttpResponseMissingData));

			var darkSkyService = new DarkSkyService("fakekey", mockCLient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			Assert.NotNull(forecast);

			// Check Response (basic deserialization check)
			Assert.NotNull(forecast.Response);
			Assert.NotNull(forecast.Response.Daily);
			Assert.Null(forecast.Response.Daily.Data);
		}

		[Fact]
		public async Task HourlyBlockEmptyTest()
		{
			var mockCLient = new Mock<IHttpClient>();
			mockCLient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(_mockHttpResponseMissingData));

			var darkSkyService = new DarkSkyService("fakekey", mockCLient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			Assert.NotNull(forecast);

			// Check Response (basic deserialization check)
			Assert.NotNull(forecast.Response);
			Assert.NotNull(forecast.Response.Hourly);
			Assert.Null(forecast.Response.Hourly.Data);
		}

		[Fact]
		public async Task MinutelyBlockEmptyTest()
		{
			var mockCLient = new Mock<IHttpClient>();
			mockCLient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(_mockHttpResponseMissingData));

			var darkSkyService = new DarkSkyService("fakekey", mockCLient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			Assert.NotNull(forecast);

			// Check Response (basic deserialization check)
			Assert.NotNull(forecast.Response);
			Assert.NotNull(forecast.Response.Minutely);
			Assert.Null(forecast.Response.Minutely.Data);
		}

		[Fact]
		public async Task ApiCallsHeaderBadValueTest()
		{
			var mockCLient = new Mock<IHttpClient>();
			mockCLient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(MockResponse()));

			HttpResponseMessage MockResponse()
			{
				var response = _mockHttpResponse;
				response.Headers.Add("X-Forecast-API-Calls", "cows");
				return response;
			}

			var darkSkyService = new DarkSkyService("fakekey", mockCLient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			// Check Headers (match pre-defined values)
			Assert.NotNull(forecast.Headers);
			Assert.Null(forecast.Headers.ApiCalls);
		}

		[Fact]
		public async Task ApiCallsHeaderNullValueTest()
		{
			var mockCLient = new Mock<IHttpClient>();
			mockCLient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(MockResponse()));

			HttpResponseMessage MockResponse()
			{
				var response = _mockHttpResponse;
				response.Headers.Add("X-Forecast-API-Calls", (string)null);
				return response;
			}

			var darkSkyService = new DarkSkyService("fakekey", mockCLient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			// Check Headers (match pre-defined values)
			Assert.NotNull(forecast.Headers);
			Assert.Null(forecast.Headers.ApiCalls);
		}

		[Fact]
		public async Task ApiCallsHeaderMissingTest()
		{
			var mockCLient = new Mock<IHttpClient>();
			mockCLient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(_mockHttpResponse));

			var darkSkyService = new DarkSkyService("fakekey", mockCLient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			// Check Headers (match pre-defined values)
			Assert.NotNull(forecast.Headers);
			Assert.Null(forecast.Headers.ApiCalls);
		}

		[Fact]
		public async Task ResponseTimeHeaderTest()
		{
			var responseTimeText = "30ms";
			var mockCLient = new Mock<IHttpClient>();
			mockCLient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(MockResponse()));

			HttpResponseMessage MockResponse()
			{
				var response = _mockHttpResponse;
				response.Headers.Add("X-Response-Time", responseTimeText);
				return response;
			}

			var darkSkyService = new DarkSkyService("fakekey", mockCLient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			// Check Headers (match pre-defined values)
			Assert.NotNull(forecast.Headers);
			Assert.Equal(responseTimeText, forecast.Headers.ResponseTime);
		}

		[Fact]
		public async Task ResponseTimeHeaderMissingTest()
		{
			var mockCLient = new Mock<IHttpClient>();
			mockCLient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(_mockHttpResponse));

			var darkSkyService = new DarkSkyService("fakekey", mockCLient.Object);
			var forecast = await darkSkyService.GetForecast(_latitude, _longitude);

			// Check Headers (match pre-defined values)
			Assert.NotNull(forecast.Headers);
			Assert.Null(forecast.Headers.ResponseTime);
		}
	}
}