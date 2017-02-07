using DarkSky.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.FormattableString;

namespace DarkSky.Services
{
	public class DarkSkyService
	{
		readonly string _apiKey;
		readonly IHttpClient _httpClient;

		/// <summary>
		/// A wrapper for the Dark Sky API.
		/// </summary>
		/// <param name="apiKey">Your API key for the Dark Sky API.</param>
		/// <param name="httpClient">An optional HTTP client to contact an API with (useful for mocking data for testing).</param>
		public DarkSkyService(string apiKey, IHttpClient httpClient = null)
		{
			if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException($"{nameof(apiKey)} cannot be empty.");
			_apiKey = apiKey;
			_httpClient = httpClient ?? new ZipHttpClient("https://api.darksky.net/");
		}

		/// <summary>
		/// Make a request to get forecast data.
		/// </summary>
		/// <param name="latitude">Latitude to request data for in decimal degrees.</param>
		/// <param name="longitude">Longitude to request data for in decimal degrees.</param>
		/// <param name="parameters">The OptionalParameters to use for the request.</param>
		/// <returns>A DarkSkyResponse with the API headers and data.</returns>
		public async Task<DarkSkyResponse> GetForecast(double latitude, double longitude, OptionalParameters parameters = null)
		{
			var requestString = BuildRequestUri(latitude, longitude, parameters);
			var response = await _httpClient.HttpRequest(requestString);
			var responseContent = await response.Content.ReadAsStringAsync();

			long callsParsed;
			return new DarkSkyResponse
			{
				Response = JsonConvert.DeserializeObject<Forecast>(responseContent),
				Headers = new DarkSkyResponse.ResponseHeaders
				{
					CacheControl = response.Headers.CacheControl,
					ApiCalls = long.TryParse(response.Headers.GetValues("X-Forecast-API-Calls")?.FirstOrDefault(), out callsParsed) ?
								(long?)callsParsed :
								null,
					ResponseTime = response.Headers.GetValues("X-Response-Time")?.FirstOrDefault()
				}
			};
		}

		string BuildRequestUri(double latitude, double longitude, OptionalParameters parameters)
		{
			var queryString = new StringBuilder(Invariant($"forecast/{_apiKey}/{latitude:N4},{longitude:N4}"));
			if (parameters?.UnixTimeInSeconds != null)
			{
				queryString.Append($",{parameters.UnixTimeInSeconds}");
			}

			if (parameters != null)
			{
				queryString.Append("?");
				if (parameters.DataBlocksToExclude != null)
				{
					queryString.Append($"&exclude={String.Join(",", parameters.DataBlocksToExclude)}");
				}
				if (parameters.ExtendHourly != null && parameters.ExtendHourly.Value)
				{
					queryString.Append("&extend=hourly");
				}
				if (!String.IsNullOrWhiteSpace(parameters.LanguageCode))
				{
					queryString.Append($"&lang={parameters.LanguageCode}");
				}
				if (!String.IsNullOrWhiteSpace(parameters.MeasurementUnits))
				{
					queryString.Append($"&units={parameters.MeasurementUnits}");
				}
			}

			return queryString.ToString();
		}

		public class OptionalParameters
		{
			public List<string> DataBlocksToExclude { get; set; }
			public bool? ExtendHourly { get; set; }
			public string LanguageCode { get; set; }
			public string MeasurementUnits { get; set; }
			public long? UnixTimeInSeconds { get; set; }
		}
	}
}