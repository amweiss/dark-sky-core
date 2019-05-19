namespace DarkSky.Tests.IntegrationTests.Services
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Threading.Tasks;
	using DarkSky.Models;
	using DarkSky.Services;
	using Microsoft.Extensions.Configuration;
	using Newtonsoft.Json;
	using Xunit;

	public class DarkSkyServiceIntegrationTests : IDisposable
	{
		readonly string _apiEnvVar = "DarkSkyApiKey";
		readonly double _latitude = 42.915;
		readonly double _longitude = -78.741;
		private DarkSkyService _darkSky;

		public DarkSkyServiceIntegrationTests()
		{
			var configBuilder = new ConfigurationBuilder().AddEnvironmentVariables();
			var config = configBuilder.Build();
			var apiKey = config.GetValue<string>(_apiEnvVar);
			Assert.False(string.IsNullOrWhiteSpace(apiKey), $"You must set the environment variable {_apiEnvVar}");
			_darkSky = new DarkSkyService(apiKey);
		}

		[Fact]
		public async Task RawResponseIsExpectedJson()
		{
			var forecast = await _darkSky.GetDarkSkyResponse(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ExtendHourly = true,
				DataBlocksToExclude = new List<ExclusionBlock> { ExclusionBlock.Flags },
				LanguageCode = "x-pig-latin",
				MeasurementUnits = "si",
			});
			var model = JsonConvert.DeserializeObject<Forecast>(forecast.RawResponse);

			Assert.NotNull(forecast);
			Assert.NotNull(forecast.Headers);
			Assert.IsType<Forecast>(model);
		}

		[Fact]
		public async Task BuffaloForecastCombineAllOptions()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ExtendHourly = true,
				DataBlocksToExclude = new List<ExclusionBlock> { ExclusionBlock.Flags },
				LanguageCode = "x-pig-latin",
				MeasurementUnits = "si",
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
			CultureInfo.CurrentCulture = new CultureInfo("de-DE");
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ExtendHourly = true,
				DataBlocksToExclude = new List<ExclusionBlock> { ExclusionBlock.Flags },
				LanguageCode = "x-pig-latin",
				MeasurementUnits = "si",
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
			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				MissingMemberHandling = MissingMemberHandling.Error,
			};
			var forecast = await _darkSky.GetForecast(_latitude, _longitude);
		}

		[Fact]
		public async Task BuffaloForecastExclude()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				DataBlocksToExclude = new List<ExclusionBlock> { ExclusionBlock.Daily, ExclusionBlock.Hourly },
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
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ExtendHourly = true,
			});

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
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				LanguageCode = "x-pig-latin",
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
			Assert.NotEmpty(forecast.Response.Hourly.Data);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
		}

		[Fact]
		public async Task BuffaloForecastTimeMachine()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ForecastDateTime = DateTime.UtcNow.AddHours(2),
			});

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
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ForecastDateTime = DateTime.Now.AddHours(2),
			});

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
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				ForecastDateTime = DateTime.UtcNow.AddHours(2),
			});

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
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				MeasurementUnits = "si",
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
				DataBlocksToExclude = new List<ExclusionBlock> { ExclusionBlock.Flags },
				LanguageCode = "x-pig-latin",
				MeasurementUnits = "si",
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

		public void Dispose()
		{
			_darkSky = null;
			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				MissingMemberHandling = MissingMemberHandling.Ignore,
			};
			CultureInfo.CurrentCulture = new CultureInfo("en-US");
		}

		[Fact]
		public async Task HandleInvalidApiKey()
		{
			// Use a different client to create one with an invalid API key.
			var client = new DarkSkyService("ThisIsAFakeKey");

			var forecast = await client.GetForecast(_latitude, _longitude);

			Assert.NotNull(forecast);
			Assert.False(forecast.IsSuccessStatus);
			Assert.NotEmpty(forecast.ResponseReasonPhrase);
		}
	}
}