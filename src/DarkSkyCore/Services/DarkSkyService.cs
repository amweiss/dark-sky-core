using System;
using System.Net.Http;
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

        readonly string _dataSource = "https://darksky.net/poweredby/";
        readonly string _attributionLine = "Powered by Dark Sky";

        public DarkSkyService(string apiKey, string darkSkyApiBaseUrl = "https://api.darksky.net/")
		{
			if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException($"{nameof(apiKey)} cannot be empty.");
            _apiKey = apiKey;
            _darkSkyApiBaseUrl = darkSkyApiBaseUrl;
        }

		async Task<JObject> GetAsJObject(double latitude, double longitude) {
            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri(_darkSkyApiBaseUrl);
                var queryString = $"forecast/{_apiKey}/{latitude:N4},{longitude:N4}"; //TODO: Add parameters
                var response = await client.GetAsync(queryString);
                if (!response.IsSuccessStatusCode) return null;
                var responseJson = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseJson);
            }
        }

        public async Task<Forecast> GetForecast(double latitude, double longitude, string exclue = null, string extend = null, string lang = null, string units = null)
		{
			var parsedResponse = await GetAsJObject(latitude, longitude);
            var forecast = JsonConvert.DeserializeObject<Forecast>(parsedResponse.ToString());

            var currently = parsedResponse["currently"];
            Console.WriteLine(currently.ToString());
            Console.WriteLine(forecast.currently.dewPoint);

            return forecast;
        }
	}
}
