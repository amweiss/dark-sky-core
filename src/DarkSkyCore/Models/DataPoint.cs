namespace DarkSky.Models
{
	using Newtonsoft.Json;

	public class DataPoint
	{
		[JsonProperty(PropertyName = "apparentTemperature")]
		public double? ApparentTemperature { get; set; }

		[JsonProperty(PropertyName = "apparentTemperatureMax")]
		public double? ApparentTemperatureMax { get; set; }

		[JsonProperty(PropertyName = "apparentTemperatureMaxTime")]
		public long? ApparentTemperatureMaxTime { get; set; }

		[JsonProperty(PropertyName = "apparentTemperatureMin")]
		public double? ApparentTemperatureMin { get; set; }

		[JsonProperty(PropertyName = "apparentTemperatureMinTime")]
		public long? ApparentTemperatureMinTime { get; set; }

		[JsonProperty(PropertyName = "cloudCover")]
		public double? CloudCover { get; set; }

		[JsonProperty(PropertyName = "dewPoint")]
		public double? DewPoint { get; set; }

		[JsonProperty(PropertyName = "humidity")]
		public double? Humidity { get; set; }

		[JsonProperty(PropertyName = "icon")]
		public string Icon { get; set; }

		[JsonProperty(PropertyName = "moonPhase")]
		public double? MoonPhase { get; set; }

		[JsonProperty(PropertyName = "nearestStormBearing")]
		public int? NearestStormBearing { get; set; }

		[JsonProperty(PropertyName = "nearestStormDistance")]
		public double? NearestStormDistance { get; set; }

		[JsonProperty(PropertyName = "ozone")]
		public double? Ozone { get; set; }

		[JsonProperty(PropertyName = "precipAccumulation")]
		public double? PrecipAccumulation { get; set; }

		[JsonProperty(PropertyName = "precipIntensity")]
		public double? PrecipIntensity { get; set; }

		[System.Obsolete("precipIntensityError is no longer provided by the DarkSky API.")]
		[JsonPropertyAttribute(PropertyName = "precipIntensityError")]
		public double? PrecipIntensityError { get; set; }

		[JsonProperty(PropertyName = "precipIntensityMax")]
		public double? PrecipIntensityMax { get; set; }

		[JsonProperty(PropertyName = "precipIntensityMaxTime")]
		public long? PrecipIntensityMaxTime { get; set; }

		[JsonProperty(PropertyName = "precipProbability")]
		public double? PrecipProbability { get; set; }

		[JsonProperty(PropertyName = "precipType")]
		public string PrecipType { get; set; }

		[JsonProperty(PropertyName = "pressure")]
		public double? Pressure { get; set; }

		[JsonProperty(PropertyName = "summary")]
		public string Summary { get; set; }

		[JsonProperty(PropertyName = "sunriseTime")]
		public long? SunriseTime { get; set; }

		[JsonProperty(PropertyName = "sunsetTime")]
		public long? SunsetTime { get; set; }

		[JsonProperty(PropertyName = "temperature")]
		public double? Temperature { get; set; }

		[JsonProperty(PropertyName = "temperatureMax")]
		public double? TemperatureMax { get; set; }

		[JsonProperty(PropertyName = "temperatureMaxTime")]
		public long? TemperatureMaxTime { get; set; }

		[JsonProperty(PropertyName = "temperatureMin")]
		public double? TemperatureMin { get; set; }

		[JsonProperty(PropertyName = "temperatureMinTime")]
		public long? TemperatureMinTime { get; set; }

		[JsonProperty(PropertyName = "time")]
		public long Time { get; set; }

		[JsonProperty(PropertyName = "uvIndex")]
		public int? UvIndex { get; set; }

		[JsonProperty(PropertyName = "uvIndexTime")]
		public int? UvIndexTime { get; set; }

		[JsonProperty(PropertyName = "visibility")]
		public double? Visibility { get; set; }

		[JsonProperty(PropertyName = "windBearing")]
		public int? WindBearing { get; set; }

		[JsonProperty(PropertyName = "windGust")]
		public double? WindGust { get; set; }

		[JsonProperty(PropertyName = "windGustTime")]
		public long? WindGustTime { get; set; }

		[JsonProperty(PropertyName = "windSpeed")]
		public double? WindSpeed { get; set; }
	}
}
