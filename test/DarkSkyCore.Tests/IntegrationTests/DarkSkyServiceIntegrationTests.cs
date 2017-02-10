using DarkSky.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Globalization;

namespace DarkSky.IntegrationTests.Services
{
	public class DarkSkyServiceIntegrationTests : IDisposable
	{
		readonly string _apiEnvVar = "DarkSkyApiKey";
		readonly double _latitude = 42.915;
		readonly double _longitude = -78.741;
		DarkSkyService _darkSky;

		public DarkSkyServiceIntegrationTests()
		{
			var configBuilder = new ConfigurationBuilder()
				.AddEnvironmentVariables();
			var config = configBuilder.Build();
			var apiKey = config.GetValue<string>(_apiEnvVar);
			Assert.False(string.IsNullOrWhiteSpace(apiKey), $"You must set the environment variable {_apiEnvVar}");
			_darkSky = new DarkSkyService(apiKey);
		}

		[Fact]
		public async Task BuffaloForecastCombineAllOptions()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ExtendHourly = true,
				DataBlocksToExclude = new List<string> { "flags" },
				LanguageCode = "x-pig-latin",
				MeasurementUnits = "si"
			});

			Assert.NotNull(forecast);
			Assert.NotNull(forecast.Response);
			Assert.NotNull(forecast.Headers);
			Assert.Equal(forecast.Response.Daily.Data.Count, 8);
			Assert.Equal(forecast.Response.Hourly.Data.Count, 169);
			Assert.Null(forecast.Response.Flags);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
		}

		[Fact]
		public async Task BuffaloForecastCombineAllOptionsGermanCulture()
		{
			CultureInfo.CurrentCulture = new CultureInfo("de-DE");
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ExtendHourly = true,
				DataBlocksToExclude = new List<string> { "flags" },
				LanguageCode = "x-pig-latin",
				MeasurementUnits = "si"
			});

			Assert.NotNull(forecast);
			Assert.NotNull(forecast.Response);
			Assert.NotNull(forecast.Headers);
			Assert.Equal(forecast.Response.Daily.Data.Count, 8);
			Assert.Equal(forecast.Response.Hourly.Data.Count, 169);
			Assert.Null(forecast.Response.Flags);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
		}

		[Fact]
		public async Task BuffaloForecastEverythingParsed()
		{
			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				MissingMemberHandling = MissingMemberHandling.Error
			};
			var forecast = await _darkSky.GetForecast(_latitude, _longitude);
		}

		[Fact]
		public async Task BuffaloForecastExclude()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				DataBlocksToExclude = new List<string> { "daily" }
			});

			Assert.NotNull(forecast);
			Assert.NotNull(forecast.Response);
			Assert.NotNull(forecast.Headers);
			Assert.Null(forecast.Response.Daily);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
		}

		[Fact]
		public async Task BuffaloForecastExtendHourly()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ExtendHourly = true
			});

			Assert.NotNull(forecast);
			Assert.NotNull(forecast.Response);
			Assert.NotNull(forecast.Headers);
			Assert.Equal(forecast.Response.Hourly.Data.Count, 169);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
		}

		[Fact]
		public async Task BuffaloForecastPigLatin()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				LanguageCode = "x-pig-latin"
			});

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
			Assert.Equal(forecast.Response.Hourly.Data.Count, 49);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
		}

		[Fact]
		public async Task BuffaloForecastTimeMachine()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ForecastDateTime = DateTime.UtcNow.AddHours(2)
			});

			Assert.NotNull(forecast);
			Assert.NotNull(forecast.Response);
			Assert.NotNull(forecast.Headers);
			Assert.Equal(forecast.Response.Daily.Data.Count, 1);
			Assert.Null(forecast.Response.Minutely);
			// Contrary to documentation, Alerts is not always omitted for time machine requests.
			// Assert.Null(forecast.Response.Alerts);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
		}

		[Fact]
		public async Task BuffaloForecastUnits()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				MeasurementUnits = "si"
			});

			Assert.NotNull(forecast);
			Assert.NotNull(forecast.Response);
			Assert.NotNull(forecast.Headers);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
		}

		[Fact]
		public async Task BuffaloTimeMachineForecastCombineAllOptions()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ForecastDateTime = DateTime.UtcNow.AddHours(2),
				DataBlocksToExclude = new List<string> { "flags" },
				LanguageCode = "x-pig-latin",
				MeasurementUnits = "si"
			});

			Assert.NotNull(forecast);
			Assert.NotNull(forecast.Response);
			Assert.NotNull(forecast.Headers);
			Assert.Equal(forecast.Response.Daily.Data.Count, 1);
			Assert.Equal(forecast.Response.Hourly.Data.Count, 24);
			Assert.Null(forecast.Response.Minutely);
			// Contrary to documentation, Alerts is not always omitted for time machine requests.
			// Assert.Null(forecast.Response.Alerts);
			Assert.Null(forecast.Response.Flags);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
		}

		public void Dispose()
		{
			_darkSky = null;
			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				MissingMemberHandling = MissingMemberHandling.Ignore
			};
			CultureInfo.CurrentCulture = new CultureInfo("en-US");
		}
	}
}