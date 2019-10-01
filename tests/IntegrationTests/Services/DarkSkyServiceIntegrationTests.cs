namespace DarkSkyCore.Tests.IntegrationTests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using DarkSky.Models;
    using DarkSky.Services;
    using DarkSkyCore.Tests.Fixtures;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public sealed class DarkSkyServiceIntegrationTests : IDisposable
    {
        public DarkSkyServiceIntegrationTests()
        {
            var configBuilder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = configBuilder.Build();
            _apiKey = config.GetValue<string>(_apiEnvVar);
            Assert.False(string.IsNullOrWhiteSpace(_apiKey), $"You must set the environment variable {_apiEnvVar}");
            _darkSky = new DarkSkyService(_apiKey);
        }

        /// <summary>
        ///     Public access to start disposing of the class instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DarkSkyServiceIntegrationTests()
        {
            Dispose(false);
        }

        private const string _apiEnvVar = "DarkSkyApiKey";
        private string _apiKey = null;
        private const double _latitude = 42.915;
        private const double _longitude = -78.741;
        private readonly DarkSkyService _darkSky;
        private bool disposedValue;

        /// <summary>
        ///     Dispose of resources used by the class.
        /// </summary>
        /// <param name="disposing">If the class is disposing managed resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                return;
            }

            if (disposing)
            {
                _darkSky.Dispose();
            }

            disposedValue = true;
        }

        [Fact]
        public async Task BuffaloForecastCombineAllOptions()
        {
            var forecast = await _darkSky.GetForecast(_latitude, _longitude,
                new OptionalParameters
                {
                    ExtendHourly = true,
                    DataBlocksToExclude = new List<ExclusionBlocks> {ExclusionBlocks.Flags},
                    LanguageCode = "x-pig-latin",
                    MeasurementUnits = "si"
                });

            Assert.NotNull(forecast);
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Headers);
            Assert.NotEmpty(forecast.Response.Daily.Data);
            Assert.NotEmpty(forecast.Response.Hourly.Data);
            Assert.Null(forecast.Response.Flags);
            Assert.Equal(forecast.Response.Latitude, _latitude);
            Assert.Equal(forecast.Response.Longitude, _longitude);
        }

        [Fact]
        public async Task BuffaloForecastCombineAllOptionsGermanCulture()
        {
            using var germanCultureParse =
                new DarkSkyService(_apiKey, jsonSerializerService: new CultureSettingFixture());

            var forecast = await germanCultureParse.GetForecast(_latitude, _longitude,
                new OptionalParameters
                {
                    ExtendHourly = true,
                    DataBlocksToExclude = new List<ExclusionBlocks> {ExclusionBlocks.Flags},
                    LanguageCode = "x-pig-latin",
                    MeasurementUnits = "si"
                });

            Assert.NotNull(forecast);
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Headers);
            Assert.NotEmpty(forecast.Response.Daily.Data);
            Assert.NotEmpty(forecast.Response.Hourly.Data);
            Assert.Null(forecast.Response.Flags);
            Assert.Equal(forecast.Response.Latitude, _latitude);
            Assert.Equal(forecast.Response.Longitude, _longitude);
        }

        [Fact]
        public async Task BuffaloForecastEverythingParsed()
        {
            using var parseAllJson =
                new DarkSkyService(_apiKey, jsonSerializerService: new MissingMemberHandlingFixture());

            await parseAllJson.GetForecast(_latitude, _longitude);
        }

        [Fact]
        public async Task BuffaloForecastExclude()
        {
            var forecast = await _darkSky.GetForecast(_latitude, _longitude,
                new OptionalParameters
                {
                    DataBlocksToExclude = new List<ExclusionBlocks> {ExclusionBlocks.Daily, ExclusionBlocks.Hourly}
                });

            Assert.NotNull(forecast);
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Headers);
            Assert.Null(forecast.Response.Daily);
            Assert.Null(forecast.Response.Hourly);
            Assert.Equal(forecast.Response.Latitude, _latitude);
            Assert.Equal(forecast.Response.Longitude, _longitude);
        }

        [Fact]
        public async Task BuffaloForecastExtendHourly()
        {
            var forecast =
                await _darkSky.GetForecast(_latitude, _longitude, new OptionalParameters {ExtendHourly = true});

            Assert.NotNull(forecast);
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Headers);
            Assert.NotEmpty(forecast.Response.Hourly.Data);
            Assert.Equal(forecast.Response.Latitude, _latitude);
            Assert.Equal(forecast.Response.Longitude, _longitude);
        }

        [Fact]
        public async Task BuffaloForecastPigLatin()
        {
            var forecast = await _darkSky.GetForecast(_latitude, _longitude,
                new OptionalParameters {LanguageCode = "x-pig-latin"});

            Assert.NotNull(forecast);
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Headers);
            Assert.Equal(forecast.Response.Latitude, _latitude);
            Assert.Equal(forecast.Response.Longitude, _longitude);
        }

        [Fact]
        public async Task BuffaloForecastReturnsFullObject()
        {
            var forecast = await _darkSky.GetForecast(_latitude, _longitude);
            Assert.NotNull(forecast);
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Headers);
            Assert.NotEmpty(forecast.Response.Hourly.Data);
            Assert.Equal(forecast.Response.Latitude, _latitude);
            Assert.Equal(forecast.Response.Longitude, _longitude);
        }

        [Fact]
        public async Task BuffaloForecastTimeMachine()
        {
            var forecast = await _darkSky.GetForecast(_latitude, _longitude,
                new OptionalParameters {ForecastDateTime = DateTime.UtcNow.AddHours(2)});

            Assert.NotNull(forecast);
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Headers);
            Assert.Single(forecast.Response.Daily.Data);
            Assert.Null(forecast.Response.Minutely);
            Assert.Equal(forecast.Response.Latitude, _latitude);
            Assert.Equal(forecast.Response.Longitude, _longitude);

            // Contrary to documentation, Alerts is not always omitted for time machine requests. Assert.Null(forecast.Response.Alerts);
        }

        [Fact]
        public async Task BuffaloForecastTimeMachineGermanCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("de-DE");
            var forecast = await _darkSky.GetForecast(_latitude, _longitude,
                new OptionalParameters {ForecastDateTime = DateTime.UtcNow.AddHours(2)});

            Assert.NotNull(forecast);
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Headers);
            Assert.Single(forecast.Response.Daily.Data);
            Assert.Null(forecast.Response.Minutely);
            Assert.Equal(forecast.Response.Latitude, _latitude);
            Assert.Equal(forecast.Response.Longitude, _longitude);

            // Contrary to documentation, Alerts is not always omitted for time machine requests. Assert.Null(forecast.Response.Alerts);
        }

        [Fact]
        public async Task BuffaloForecastTimeMachineLocalTimezone()
        {
            var forecast = await _darkSky.GetForecast(_latitude, _longitude,
                new OptionalParameters {ForecastDateTime = DateTime.Now.AddHours(2)});

            Assert.NotNull(forecast);
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Headers);
            Assert.Single(forecast.Response.Daily.Data);
            Assert.Null(forecast.Response.Minutely);
            Assert.Equal(forecast.Response.Latitude, _latitude);
            Assert.Equal(forecast.Response.Longitude, _longitude);

            // Contrary to documentation, Alerts is not always omitted for time machine requests. Assert.Null(forecast.Response.Alerts);
        }

        [Fact]
        public async Task BuffaloForecastUnits()
        {
            var forecast =
                await _darkSky.GetForecast(_latitude, _longitude, new OptionalParameters {MeasurementUnits = "si"});

            Assert.NotNull(forecast);
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Headers);
            Assert.Equal(forecast.Response.Latitude, _latitude);
            Assert.Equal(forecast.Response.Longitude, _longitude);
        }

        [Fact]
        public async Task BuffaloTimeMachineForecastCombineAllOptions()
        {
            var forecast = await _darkSky.GetForecast(_latitude, _longitude,
                new OptionalParameters
                {
                    ForecastDateTime = DateTime.UtcNow.AddHours(2),
                    DataBlocksToExclude = new List<ExclusionBlocks> {ExclusionBlocks.Flags},
                    LanguageCode = "x-pig-latin",
                    MeasurementUnits = "si"
                });

            Assert.NotNull(forecast);
            Assert.NotNull(forecast.Response);
            Assert.NotNull(forecast.Headers);
            Assert.Single(forecast.Response.Daily.Data);
            Assert.NotEmpty(forecast.Response.Hourly.Data);
            Assert.Null(forecast.Response.Minutely);
            Assert.Null(forecast.Response.Flags);
            Assert.Equal(forecast.Response.Latitude, _latitude);
            Assert.Equal(forecast.Response.Longitude, _longitude);

            // Contrary to documentation, Alerts is not always omitted for time machine requests. Assert.Null(forecast.Response.Alerts);
        }
    }
}