namespace DarkSky.Services
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using static System.FormattableString;

    /// <summary>
    ///     Wrapper class for interacting with the Dark Sky API.
    /// </summary>
    public class DarkSkyService : IDisposable
    {
        private readonly string apiKey;
        private readonly Uri baseUri;
        private readonly IHttpClient httpClient;
        private readonly IJsonSerializerService jsonSerializerService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DarkSkyService" /> class. A wrapper for the
        ///     Dark Sky API.
        /// </summary>
        /// <param name="apiKey">Your API key for the Dark Sky API.</param>
        /// <param name="baseUri">The base URI for the Dark Sky API. Defaults to https://api.darksky.net/ .</param>
        /// <param name="httpClient">
        ///     An optional HTTP client to contact an API with (useful for mocking data for testing).
        /// </param>
        /// <param name="jsonSerializerService">
        ///     An optional JSON Serializer to handle converting the response string to an object.
        /// </param>
        public DarkSkyService(string apiKey, Uri baseUri = null, IHttpClient httpClient = null, IJsonSerializerService jsonSerializerService = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException($"{nameof(apiKey)} cannot be empty.");
            }

            this.apiKey = apiKey;
            this.baseUri = baseUri ?? new Uri("https://api.darksky.net/");
            this.httpClient = httpClient ?? new ZipHttpClient();
            this.jsonSerializerService = jsonSerializerService
                ?? new JsonNetJsonSerializerService();
        }

        /// <summary>
        ///     Destructor
        /// </summary>
        ~DarkSkyService()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Make a request to get forecast data.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees.</param>
        /// <param name="parameters">The OptionalParameters to use for the request.</param>
        /// <returns>A DarkSkyResponse with the API headers and data.</returns>
        public async Task<DarkSkyResponse> GetForecast(double latitude, double longitude,
            OptionalParameters parameters = null)
        {
            var requestString = BuildRequestUri(latitude, longitude, parameters);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{requestString}").ConfigureAwait(false);
            var responseContent = response.Content?.ReadAsStringAsync();

            var darkSkyResponse = new DarkSkyResponse
            {
                IsSuccessStatus = response.IsSuccessStatusCode, ResponseReasonPhrase = response.ReasonPhrase
            };

            if (!darkSkyResponse.IsSuccessStatus)
            {
                return darkSkyResponse;
            }

            try
            {
                darkSkyResponse.Response =
                    await jsonSerializerService.DeserializeJsonAsync<Forecast>(responseContent).ConfigureAwait(false);
            }
            catch (FormatException e)
            {
                darkSkyResponse.Response = null;
                darkSkyResponse.IsSuccessStatus = false;
                darkSkyResponse.ResponseReasonPhrase = $"Error parsing results: {e?.InnerException?.Message ?? e.Message}";
            }

            response.Headers.TryGetValues("X-Forecast-API-Calls", out var apiCallsHeader);
            response.Headers.TryGetValues("X-Response-Time", out var responseTimeHeader);

            darkSkyResponse.Headers = new ResponseHeaders
            {
                CacheControl = response.Headers.CacheControl,
                ApiCalls = long.TryParse(apiCallsHeader?.FirstOrDefault(), out var callsParsed)
                    ? (long?)callsParsed
                    : null,
                ResponseTime = responseTimeHeader?.FirstOrDefault()
            };

            if (darkSkyResponse.Response == null)
            {
                return darkSkyResponse;
            }

            if (darkSkyResponse.Response.Currently != null)
            {
                darkSkyResponse.Response.Currently.TimeZone = darkSkyResponse.Response.TimeZone;
            }

            darkSkyResponse.Response.Alerts?.ForEach(a => a.TimeZone = darkSkyResponse.Response.TimeZone);
            darkSkyResponse.Response.Daily?.Data?.ForEach(d => d.TimeZone = darkSkyResponse.Response.TimeZone);
            darkSkyResponse.Response.Hourly?.Data?.ForEach(h => h.TimeZone = darkSkyResponse.Response.TimeZone);
            darkSkyResponse.Response.Minutely?.Data?.ForEach(
                m => m.TimeZone = darkSkyResponse.Response.TimeZone);

            return darkSkyResponse;
        }

        private string BuildRequestUri(double latitude, double longitude, OptionalParameters parameters)
        {
            var queryString = new StringBuilder(Invariant($"forecast/{apiKey}/{latitude:N4},{longitude:N4}"));
            if (parameters?.ForecastDateTime != null)
            {
                queryString.Append(
                    $",{parameters.ForecastDateTime.Value.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture)}");
            }

            if (parameters == null)
            {
                return queryString.ToString();
            }

            queryString.Append("?");
            if (parameters.DataBlocksToExclude != null)
            {
                queryString.Append(
                    $"&exclude={string.Join(",", parameters.DataBlocksToExclude.Select(x => x.ToString().ToLowerInvariant()))}");
            }

            if (parameters.ExtendHourly != null && parameters.ExtendHourly.Value)
            {
                queryString.Append("&extend=hourly");
            }

            if (!string.IsNullOrWhiteSpace(parameters.LanguageCode))
            {
                queryString.Append($"&lang={parameters.LanguageCode}");
            }

            if (!string.IsNullOrWhiteSpace(parameters.MeasurementUnits))
            {
                queryString.Append($"&units={parameters.MeasurementUnits}");
            }

            return queryString.ToString();
        }

        #region IDisposable Support

        private bool disposedValue; // To detect redundant calls

        /// <summary>
        ///     Dispose of resources used by the class.
        /// </summary>
        /// <param name="disposing">If the class is disposing managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    httpClient.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        ///     Public access to start disposing of the class instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}