using System;
using DarkSky.Models;

namespace DarkSky.Services
{
	public class DarkSkyService
	{
		public Forecast GetForecast(string key, double latitude, double longitude, string exclue = null, string extend = null, string lang = null, string units = null)
		{
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException($"API Key parameter: {nameof(key)} cannot be empty.");
            return null;
        }
	}
}
