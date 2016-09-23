using System;

namespace DarkSky.Models
{
	public class DataPoint
	{
		public double? apparentTemperature { get; set; }
		public double? apparentTemperatureMax { get; set; }
		public long? apparentTemperatureMaxTime { get; set; }
		public long? apparentTemperatureMinTime { get; set; }
		public double? cloudCover { get; set; }
		public Double? dewPoint { get; set; }
		public double? humidity { get; set; }
		public string icon { get; set; }
		public double? moonPhase { get; set; }
		public int? nearestStormBearing { get; set; }
		public double? nearestStormDistance { get; set; }
		public double? ozone { get; set; }
		public double? precipAccumulation { get; set; }
		public double? precipIntensity { get; set; }
		public double? precipIntensityMax { get; set; }
		public long? precipIntensityMaxTime { get; set; }
		public double? precipProbability { get; set; }
		public string precipType { get; set; }
		public double? pressure { get; set; }
		public long? sunriseTime { get; set; }
		public long? sunsetTime { get; set; }
		public double? temperature { get; set; }
		public double? temperatureMax { get; set; }
		public long? temperatureMaxTime { get; set; }
		public double? temperatureMin { get; set; }
		public long? temperatureMinTime { get; set; }
		public long time { get; set; }
		public double? visibility { get; set; }
		public int? windBearing { get; set; }
		public double? windSpeed { get; set; }
    }
}