using Xunit;
using DarkSky.Services;
using Microsoft.Extensions.Configuration;

namespace DarkSky.IntegrationTests.Services
{
    public class DarkSkyServiceIntegrationTests
    {
        readonly string _apiEnvVar = "DarkSkyApiKey";
        readonly double _latitude = 42.915;
        readonly double _longitude = -78.741;
        readonly DarkSkyService _darkSky;

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
		public async void BuffaloForecastReturnsFullObject()
		{
            var forecast = await _darkSky.GetForecast(_latitude, _longitude);
            Assert.NotNull(forecast);
            Assert.Equal(forecast.latitude, _latitude);
            Assert.Equal(forecast.longitude, _longitude);

        }
    }

}