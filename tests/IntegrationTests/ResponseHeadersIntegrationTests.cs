namespace DarkSky.Tests.IntegrationTests
{
    using DarkSky.Services;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class ResponseHeadersIntegrationTests : IDisposable
    {
        private readonly string _apiEnvVar = "DarkSkyApiKey";
        private readonly double _latitude = 42.915;
        private readonly double _longitude = -78.741;
        private readonly DarkSkyService _darkSky;

        public ResponseHeadersIntegrationTests()
        {
            var configBuilder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = configBuilder.Build();
            var apiKey = config.GetValue<string>(_apiEnvVar);
            Assert.False(string.IsNullOrWhiteSpace(apiKey), $"You must set the environment variable {_apiEnvVar}");
            _darkSky = new DarkSkyService(apiKey);
        }

        [Fact]
        public async Task CacheControlHeaderNullValueTest()
        {
            var forecast = await _darkSky.GetForecast(_latitude, _longitude);
            Assert.NotNull(forecast.Headers);
            Assert.NotNull(forecast.Headers.CacheControl);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// Dispose of resources used by the class.
        /// </summary>
        /// <param name="disposing">If the class is disposing managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _darkSky.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Public access to start disposing of the class instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}