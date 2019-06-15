namespace DarkSky.Tests.UnitTests.Services
{
    using DarkSky.Services;
    using DarkSky.Tests.UnitTests.Fixtures;
    using Moq;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public class DarkSkyServiceUnitTests : IClassFixture<ResponseFixture>
    {
        private readonly ResponseFixture _fixture;

        public DarkSkyServiceUnitTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ApiCallsHeaderBadValueTest()
        {
            var mockCLient = new Mock<IHttpClient>();
            mockCLient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(MockResponse()));

            HttpResponseMessage MockResponse()
            {
                var response = ResponseFixture.MockHttpResponse;
                response.Headers.Add("X-Forecast-API-Calls", "cows");
                return response;
            }

            using (var darkSkyService = new DarkSkyService("fakekey", httpClient: mockCLient.Object))
            {
                var forecast = await darkSkyService.GetForecast(ResponseFixture.Latitude, ResponseFixture.Longitude);
                // Check Headers (match pre-defined values)
                Assert.NotNull(forecast.Headers);
                Assert.Null(forecast.Headers.ApiCalls);
            }
        }

        [Fact]
        public void ApiCallsHeaderMissingTest()
        {
            var forecast = _fixture.NormalResponse;

            // Check Headers (match pre-defined values)
            Assert.NotNull(forecast.Headers);
            Assert.Null(forecast.Headers.ApiCalls);
        }

        [Fact]
        public async Task ApiCallsHeaderNullValueTest()
        {
            var mockCLient = new Mock<IHttpClient>();
            mockCLient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(MockResponse()));

            HttpResponseMessage MockResponse()
            {
                var response = ResponseFixture.MockHttpResponse;
                response.Headers.Add("X-Forecast-API-Calls", (string)null);
                return response;
            }

            using (var darkSkyService = new DarkSkyService("fakekey", httpClient: mockCLient.Object))
            {
                var forecast = await darkSkyService.GetForecast(ResponseFixture.Latitude, ResponseFixture.Longitude);
                // Check Headers (match pre-defined values)
                Assert.NotNull(forecast.Headers);
                Assert.Null(forecast.Headers.ApiCalls);
            }
        }

        [Fact]
        public void ConstructorWithNonEmptyApiKey()
        {
            using (var darkSkyService = new DarkSkyService("fakekey"))
            {
                Assert.NotNull(darkSkyService);
            }
        }

        [Fact]
        public void DailyBlockEmptyTest()
        {
            var forecast = _fixture.MissingDataResponse;

            Assert.NotNull(forecast);

            // Check Response (basic deserialization check)
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Response.Daily);
            Assert.Null(forecast.Response.Daily.Data);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("	")]
        [InlineData("  ")]
        public void ExceptionThrownForMissingApiKey(string value) => Assert.ThrowsAny<ArgumentException>(() =>
                                                                     {
                                                                         var darkSkyService = new DarkSkyService(value);
                                                                         var result = darkSkyService.GetForecast(0, 0);
                                                                     });

        [Fact]
        public void GetForecastWithMockData()
        {
            var forecast = _fixture.NormalResponse;

            Assert.NotNull(forecast);

            // Check Response (basic deserialization check)
            Assert.NotNull(forecast.Response);
            Assert.Equal(forecast.Response.Latitude, ResponseFixture.Latitude);
            Assert.Equal(forecast.Response.Longitude, ResponseFixture.Longitude);
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
        public void HourlyBlockEmptyTest()
        {
            var forecast = _fixture.MissingDataResponse;

            Assert.NotNull(forecast);

            // Check Response (basic deserialization check)
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Response.Hourly);
            Assert.Null(forecast.Response.Hourly.Data);
        }

        [Fact]
        public void MinutelyBlockEmptyTest()
        {
            var forecast = _fixture.MissingDataResponse;

            Assert.NotNull(forecast);

            // Check Response (basic deserialization check)
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Response.Minutely);
            Assert.Null(forecast.Response.Minutely.Data);
        }

        [Fact]
        public void ResponseTimeHeaderMissingTest()
        {
            var forecast = _fixture.NormalResponse;

            // Check Headers (match pre-defined values)
            Assert.NotNull(forecast.Headers);
            Assert.Null(forecast.Headers.ResponseTime);
        }

        [Fact]
        public async Task ResponseTimeHeaderTest()
        {
            var responseTimeText = "30ms";
            var mockCLient = new Mock<IHttpClient>();
            mockCLient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(MockResponse()));

            HttpResponseMessage MockResponse()
            {
                var response = ResponseFixture.MockHttpResponse;
                response.Headers.Add("X-Response-Time", responseTimeText);
                return response;
            }

            using (var darkSkyService = new DarkSkyService("fakekey", httpClient: mockCLient.Object))
            {
                var forecast = await darkSkyService.GetForecast(ResponseFixture.Latitude, ResponseFixture.Longitude);
                // Check Headers (match pre-defined values)
                Assert.NotNull(forecast.Headers);
                Assert.Equal(responseTimeText, forecast.Headers.ResponseTime);
            }
        }
    }
}