using Xunit;
using DarkSky.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

namespace DarkSky.IntegrationTests.Services
{
	public class DarkSkyServiceIntegrationTests : IDisposable
	{
		readonly string _apiEnvVar = "DarkSkyApiKey";
		readonly double _latitude = 29.4264; //42.915;
		readonly double _longitude = -98.5105; //-78.741;
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

		public void Dispose()
		{
			_darkSky = null;
		}

		[Fact]
		public async void BuffaloForecastReturnsFullObject()
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
		public async void BuffaloForecastTimeMachine()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				UnixTimeInSeconds = new DateTimeOffset(DateTime.UtcNow.AddHours(2)).ToUnixTimeSeconds()
			});

			Assert.NotNull(forecast);
			Assert.NotNull(forecast.Response);
			Assert.NotNull(forecast.Headers);
			Assert.Equal(forecast.Response.Daily.Data.Count, 1);
			Assert.Null(forecast.Response.Minutely);
			Assert.Null(forecast.Response.Alerts);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
		}

		[Fact]
		public async void BuffaloForecastExclude()
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
		public async void BuffaloForecastExtendHourly()
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
		public async void BuffaloForecastPigLatin()
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
		public async void BuffaloForecastUnits()
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
		public async void BuffaloTimeMachineForecastCombineAllOptions()
		{
			var forecast = await _darkSky.GetForecast(_latitude, _longitude, new DarkSkyService.OptionalParameters
			{
				UnixTimeInSeconds = new DateTimeOffset(DateTime.UtcNow.AddHours(2)).ToUnixTimeSeconds(),
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
			Assert.Null(forecast.Response.Alerts);
			Assert.Null(forecast.Response.Flags);
			Assert.Equal(forecast.Response.Latitude, _latitude);
			Assert.Equal(forecast.Response.Longitude, _longitude);
		}

		[Fact]
		public async void BuffaloForecastCombineAllOptions()
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
	}

}