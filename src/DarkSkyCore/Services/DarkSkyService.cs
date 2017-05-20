﻿namespace DarkSky.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using DarkSky.Models;
	using Newtonsoft.Json;
	using static System.FormattableString;

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
			if (string.IsNullOrWhiteSpace(apiKey))
			{
				throw new ArgumentException($"{nameof(apiKey)} cannot be empty.");
			}

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

			var darkSkyResponse = new DarkSkyResponse()
			{
				IsSuccessful = response.IsSuccessStatusCode,
				ResponseReasonPhrase = response.ReasonPhrase,
			};

			if (darkSkyResponse.IsSuccessful)
			{
				long callsParsed;

				darkSkyResponse.Response = JsonConvert.DeserializeObject<Forecast>(responseContent);
				darkSkyResponse.Headers = new DarkSkyResponse.ResponseHeaders
				{
					CacheControl = response.Headers.CacheControl,
					ApiCalls = long.TryParse(response.Headers.GetValues("X-Forecast-API-Calls")?.FirstOrDefault(), out callsParsed) ?
						(long?)callsParsed :
						null,
					ResponseTime = response.Headers.GetValues("X-Response-Time")?.FirstOrDefault()
				};
			}

			return darkSkyResponse;
		}

		string BuildRequestUri(double latitude, double longitude, OptionalParameters parameters)
		{
			var queryString = new StringBuilder(Invariant($"forecast/{_apiKey}/{latitude:N4},{longitude:N4}"));
			if (parameters?.ForecastDateTime != null)
			{
				queryString.Append($",{parameters.ForecastDateTime.Value.ToString("yyyy-MM-ddTHH:mm:ss")}");
			}

			if (parameters != null)
			{
				queryString.Append("?");
				if (parameters.DataBlocksToExclude != null)
				{
					queryString.Append($"&exclude={string.Join(",", parameters.DataBlocksToExclude)}");
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
			}

			return queryString.ToString();
		}

		public class OptionalParameters
		{
			public List<string> DataBlocksToExclude { get; set; }
			public bool? ExtendHourly { get; set; }
			public string LanguageCode { get; set; }
			public string MeasurementUnits { get; set; }
			public DateTime? ForecastDateTime { get; set; }
		}
	}
}