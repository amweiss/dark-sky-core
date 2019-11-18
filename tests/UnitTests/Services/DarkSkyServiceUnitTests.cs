namespace DarkSkyCore.Tests.UnitTests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using DarkSky.Models;
    using DarkSky.Services;
    using Fixtures;
    using Moq;
    using Xunit;

    public class DarkSkyServiceUnitTests : IClassFixture<ResponseFixture>
    {
        public DarkSkyServiceUnitTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ResponseFixture _fixture;

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("	")]
        [InlineData("  ")]
        public void ExceptionThrownForMissingApiKey(string value) =>
            Assert.ThrowsAny<ArgumentException>(() => new DarkSkyService(value));

        [Fact]
        public void ConstructorWithNonEmptyApiKey()
        {
            using var darkSkyService = new DarkSkyService("fakekey");
            Assert.NotNull(darkSkyService);
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

        [Fact]
        public void FullQueryBuilderTest()
        {
            var expectedQuery =
                $"forecast/fakekey/{ResponseFixture.Latitude},{ResponseFixture.Longitude},1970-01-01T00:00:00Z?&exclude=alerts,flags&extend=hourly&lang=x-pig-latin&units=si";
            const string baseUri = @"https://api.darksky.net/";
            var totalQuery = $"{baseUri}{expectedQuery}";

            var queryCheckClient = new Mock<IHttpClient>();

            queryCheckClient.Setup(f => f.HttpRequestAsync(totalQuery))
                .Returns((string s) => Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK, ReasonPhrase = s
                }));

            using var darkSkyServiceQueryCheck =
                new DarkSkyService("fakekey", new Uri(baseUri), queryCheckClient.Object);
            var parameters = new OptionalParameters
            {
                DataBlocksToExclude = new List<ExclusionBlocks> { ExclusionBlocks.Alerts, ExclusionBlocks.Flags },
                ExtendHourly = true,
                LanguageCode = "x-pig-latin",
                MeasurementUnits = "si",
                ForecastDateTime = DateTime.UnixEpoch
            };
            var result = darkSkyServiceQueryCheck
                .GetForecast(ResponseFixture.Latitude, ResponseFixture.Longitude, parameters).Result;

            Assert.Equal(result.ResponseReasonPhrase, totalQuery);
            Assert.True(result.IsSuccessStatus);
        }

        [Fact]
        public void GetForecastWithAlertData()
        {
            var forecast = _fixture.AlertResponse;

            Assert.NotNull(forecast);

            // Check Response (basic deserialization check)
            Assert.NotNull(forecast.Response);
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
        public void GetForecastWithTimeMachineMockData()
        {
            var forecast = _fixture.NormalResponse;

            Assert.NotNull(forecast);

            // Check Response (basic deserialization check)
            Assert.NotNull(forecast.Response);
            Assert.Equal(ResponseFixture.Latitude, forecast.Response.Latitude);
            Assert.Equal(ResponseFixture.Longitude, forecast.Response.Longitude);
            Assert.NotNull(forecast.Response.Currently);
            Assert.NotNull(forecast.Response.Daily);
            Assert.NotEmpty(forecast.Response.Daily.Data);
            Assert.NotNull(forecast.Response.Flags);
            Assert.NotNull(forecast.Response.Hourly);
            Assert.NotEmpty(forecast.Response.Hourly.Data);
            Assert.NotNull(forecast.Response.TimeZone);
        }

        [Fact]
        public void HandleInvalidApiKey()
        {
            var forecast = _fixture.ForbiddenResponse;

            Assert.NotNull(forecast);
            Assert.False(forecast.IsSuccessStatus);
            Assert.NotEmpty(forecast.ResponseReasonPhrase);
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
            var forecast = _fixture.MissingDataResponse;

            // Check Headers (match pre-defined values)
            Assert.NotNull(forecast.Headers);
            Assert.Null(forecast.Headers.ResponseTime);
        }

        [Fact]
        public void ResponseTimeHeaderTest()
        {
            var forecast = _fixture.NormalResponse;

            // Check Headers (match pre-defined values)
            Assert.NotNull(forecast.Headers);
            Assert.Equal(ResponseFixture.ResponseTime, forecast.Headers.ResponseTime);
        }

        [Fact]
        public void DisposeTest()
        {
            var service = new DarkSkyService("ABCD");
            service.Dispose();
            service.Dispose();
        }
    }
}