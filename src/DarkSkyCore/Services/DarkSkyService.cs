namespace DarkSky.Services
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using DarkSky.Models;
	using Newtonsoft.Json;
	using static System.FormattableString;

	/// <summary>
	/// Wrapper class for interacting with the Dark Sky API.
	/// </summary>
	public partial class DarkSkyService
	{
		readonly string apiKey;
		readonly IHttpClient httpClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="DarkSkyService"/> class.
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

			this.apiKey = apiKey;
			this.httpClient = httpClient ?? new ZipHttpClient("https://api.darksky.net/");
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
			var response = await httpClient.HttpRequest(requestString);
			var responseContent = await response.Content.ReadAsStringAsync();

			var darkSkyResponse = new DarkSkyResponse()
			{
				IsSuccessStatus = response.IsSuccessStatusCode,
				ResponseReasonPhrase = response.ReasonPhrase,
			};

			if (darkSkyResponse.IsSuccessStatus)
			{
				darkSkyResponse.Response = JsonConvert.DeserializeObject<Forecast>(responseContent);
				darkSkyResponse.Headers = new DarkSkyResponse.ResponseHeaders
				{
					CacheControl = response.Headers.CacheControl,
					ApiCalls = long.TryParse(response.Headers.GetValues("X-Forecast-API-Calls")?.FirstOrDefault(), out long callsParsed) ?
						(long?)callsParsed :
						null,
					ResponseTime = response.Headers.GetValues("X-Response-Time")?.FirstOrDefault(),
#pragma warning disable CS0612 // Type or member is obsolete
					Expires = response.Content.Headers.Expires
#pragma warning restore CS0612 // Type or member is obsolete
				};
			}

			return darkSkyResponse;
		}

		string BuildRequestUri(double latitude, double longitude, OptionalParameters parameters)
		{
			var queryString = new StringBuilder(Invariant($"forecast/{apiKey}/{latitude:N4},{longitude:N4}"));
			if (parameters?.ForecastDateTime != null)
			{
				queryString.Append($",{parameters.ForecastDateTime.Value.ToString("yyyy-MM-ddTHH:mm:ss")}");
			}

			if (parameters != null)
			{
				queryString.Append("?");
				if (parameters.DataBlocksToExclude != null)
				{
					queryString.Append($"&exclude={string.Join(",", parameters.DataBlocksToExclude.Select(x => x.ToString().ToLowerInvariant()))}");
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

		/// <summary>
		/// Optional parameters that an be used to modify the API request.
		/// </summary>
		public partial class OptionalParameters
		{
			/// <summary>
			/// A List of <see cref="ExclusionBlock"/> that prevent specific <see cref="DataBlock"/> properties from being populated from the API.
			/// </summary>
			public List<ExclusionBlock> DataBlocksToExclude { get; set; }

			/// <summary>
			/// When present, return hour-by-hour data for the next 168 hours, instead of the next 48.
			/// <para>When using this option, we strongly recommend enabling HTTP compression.</para>
			/// </summary>
			public bool? ExtendHourly { get; set; }

			/// <summary>
			/// Return <see cref="DataBlock.Summary"/> properties in the desired language.
			/// <para>(Note that units in the summary will be set according to the <see cref="MeasurementUnits"/> parameter, so be sure to set both parameters appropriately.).</para>
			/// <para>English is the default, but see the <a href="https://darksky.net/dev/docs/forecast">forecast documentation page</a> for supported languages</para>
			/// </summary>
			public string LanguageCode { get; set; }

			/// <summary>
			/// Return weather conditions in the requested units.
			/// <para>US Imperial Units are the default, but see the <a href="https://darksky.net/dev/docs/forecast">forecast documentation page</a> for supported units</para>
			/// </summary>
			public string MeasurementUnits { get; set; }

			/// <summary>
			/// A Time Machine Request returns the observed (in the past) or forecasted (in the future) hour-by-hour weather and daily weather conditions for a particular date.
			/// <para>A Time Machine request is identical in structure to a <see cref="Forecast"/>, except:</para>
			/// <list type="bullet">
			/// <item><description>The currently data point will refer to the time provided, rather than the current time.</description></item>
			/// <item><description>The minutely data block will be omitted, unless you are requesting a time within an hour of the present.</description></item>
			/// <item><description>The hourly data block will contain data points starting at midnight (local time) of the day requested, and continuing until midnight (local time) of the following day.</description></item>
			/// <item><description>The daily data block will contain a single data point referring to the requested date.</description></item>
			/// <item><description>The alerts data block will be omitted.</description></item>
			/// </list>
			/// </summary>
			public DateTime? ForecastDateTime { get; set; }
		}
	}
}