using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DarkSky.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DarkSky.Services
{
    public class DarkSkyService
    {
        readonly string _apiKey;
        readonly string _darkSkyApiBaseUrl;

        class ResponseAndHeaders
        {
            internal JObject Response { get; set; }
            internal CacheControlHeaderValue CacheControl { get; set; }
            internal long? ApiCalls { get; set; }
            internal string ResponseTime { get; set; }
        }

        public class OptionalParameters
        {
            public long? UnixTimeInSeconds { get; set; }
            public List<string> DataBlocksToExclude { get; set; }
            public bool? ExtendHourly { get; set; }
            public string LanguageCode { get; set; }
            public string MeasurementUnits { get; set; }
        }

        public DarkSkyService(string apiKey, string darkSkyApiBaseUrl = "https://api.darksky.net/")
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException($"{nameof(apiKey)} cannot be empty.");
            _apiKey = apiKey;
            _darkSkyApiBaseUrl = darkSkyApiBaseUrl;
        }

        string BuildRequestUri(double latitude, double longitude, OptionalParameters parameters)
        {
            var queryString = new StringBuilder($"forecast/{_apiKey}/{latitude:N4},{longitude:N4}");
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

        async Task<ResponseAndHeaders> GetAsJObject(double latitude, double longitude, OptionalParameters parameters)
        {
            using (var handler = new HttpClientHandler())
            {
                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(_darkSkyApiBaseUrl);
                    var response = await client.GetAsync(BuildRequestUri(latitude, longitude, parameters));
                    if (!response.IsSuccessStatusCode) return null;
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var retVal =  new ResponseAndHeaders
                    {
                        Response = JObject.Parse(responseJson),
                        CacheControl = response.Headers.CacheControl,
                        ResponseTime = response.Headers.GetValues("X-Response-Time")?.FirstOrDefault()
                    };

                    long callsParsed;
                    retVal.ApiCalls = long.TryParse(response.Headers.GetValues("X-Forecast-API-Calls")?.FirstOrDefault(), out callsParsed) ?
                        (long?)callsParsed :
                        null;

                    return retVal;
                }
            }
        }

        public async Task<DarkSkyResponse> GetForecast(double latitude, double longitude, OptionalParameters paramters = null)
        {
            var responseAndHeaders = await GetAsJObject(latitude, longitude, paramters);
            var forecast = JsonConvert.DeserializeObject<Forecast>(responseAndHeaders.Response.ToString());
            
            return new DarkSkyResponse
            {
                Response = forecast,
                Headers = new DarkSkyResponse.ResponseHeaders
                {
                    CacheControl = responseAndHeaders?.CacheControl,
                    ApiCalls = responseAndHeaders?.ApiCalls,
                    ResponseTime = responseAndHeaders?.ResponseTime
                }
            };
        }
    }
}
