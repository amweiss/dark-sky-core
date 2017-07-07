namespace DarkSky.Models
{
	using System;
	using Newtonsoft.Json;

	/// <summary>
	/// A data point object contains various properties, each representing the average (unless otherwise specified) of a particular weather phenomenon occurring during a period of time: an instant in the case of <see cref="Forecast.Currently"/>, a minute for <see cref="Forecast.Minutely"/>, an hour for <see cref="Forecast.Hourly"/>, and a day for <see cref="Forecast.Daily"/>.
	/// </summary>
	public class DataPoint
	{
		/// <summary>
		/// The apparent (or “feels like”) temperature in degrees Fahrenheit.
		/// </summary>
		/// <remarks>optional, not on daily</remarks>
		[JsonProperty(PropertyName = "apparentTemperature")]
		public double? ApparentTemperature { get; set; }

		/// <summary>
		/// The maximum value of <see cref="ApparentTemperature"/> during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "apparentTemperatureMax")]
		public double? ApparentTemperatureMax { get; set; }

		/// <summary>
		/// The UNIX time of when <see cref="ApparentTemperatureMax"/> occurs during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "apparentTemperatureMaxTime")]
		public long? ApparentTemperatureMaxTime { get; set; }

		/// <summary>
		/// The minimum value of <see cref="ApparentTemperature"/> during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "apparentTemperatureMin")]
		public double? ApparentTemperatureMin { get; set; }

		/// <summary>
		/// The UNIX time of when <see cref="ApparentTemperatureMin"/> occurs during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "apparentTemperatureMinTime")]
		public long? ApparentTemperatureMinTime { get; set; }

		/// <summary>
		/// The percentage of sky occluded by clouds, between 0 and 1, inclusive.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "cloudCover")]
		public double? CloudCover { get; set; }

		/// <summary>
		/// The dew point in degrees Fahrenheit.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "dewPoint")]
		public double? DewPoint { get; set; }

		/// <summary>
		/// The relative humidity, between 0 and 1, inclusive.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "humidity")]
		public double? Humidity { get; set; }

		/// <summary>
		/// A machine-readable text summary of this data point, suitable for selecting an icon for display.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "icon")]
		public Icon Icon { get; set; }

		/// <summary>
		/// The fractional part of the lunation number during the given day: a value of 0 corresponds to a new moon, 0.25 to a first quarter moon, 0.5 to a full moon, and 0.75 to a last quarter moon.
		/// <para>(The ranges in between these represent waxing crescent, waxing gibbous, waning gibbous, and waning crescent moons, respectively.)</para>
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "moonPhase")]
		public double? MoonPhase { get; set; }

		/// <summary>
		/// The approximate direction of the nearest storm in degrees, with true north at 0° and progressing clockwise.
		/// <para>(If <see cref="NearestStormDistance"/> is zero, then this value will not be defined.)</para>
		/// </summary>
		/// <remarks>optional, only on currently</remarks>
		[JsonProperty(PropertyName = "nearestStormBearing")]
		public int? NearestStormBearing { get; set; }

		/// <summary>
		/// The approximate distance to the nearest storm in miles.
		/// <para>(A storm distance of 0 doesn’t necessarily refer to a storm at the requested location, but rather a storm in the vicinity of that location.)</para>
		/// </summary>
		/// <remarks>optional, only on currently</remarks>
		[JsonProperty(PropertyName = "nearestStormDistance")]
		public double? NearestStormDistance { get; set; }

		/// <summary>
		/// The columnar density of total atmospheric ozone at the given time in Dobson units.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "ozone")]
		public double? Ozone { get; set; }

		/// <summary>
		/// The amount of snowfall accumulation expected to occur, in inches.
		/// <para>(If no snowfall is expected, this property will not be defined.)</para>
		/// </summary>
		/// <remarks>optional, only on hourly and daily</remarks>
		[JsonProperty(PropertyName = "precipAccumulation")]
		public double? PrecipAccumulation { get; set; }

		/// <summary>
		/// The intensity (in inches of liquid water per hour) of precipitation occurring at the given time. This value is conditional on probability (that is, assuming any precipitation occurs at all) for minutely data points, and unconditional otherwise.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "precipIntensity")]
		public double? PrecipIntensity { get; set; }

		/// <summary>
		/// Undocumented, presumably was a value representing the potential error in the <see cref="PrecipIntensity"/>.
		/// </summary>
		/// <remarks>deprecated</remarks>
		[Obsolete("PrecipIntensityError is no longer provided by the DarkSky API.")]
		[JsonProperty(PropertyName = "precipIntensityError")]
		public double? PrecipIntensityError { get; set; }

		/// <summary>
		/// The maximum value of <see cref="PrecipIntensity"/> during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "precipIntensityMax")]
		public double? PrecipIntensityMax { get; set; }

		/// <summary>
		/// The UNIX time of when <see cref="PrecipIntensityMax"/> occurs during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "precipIntensityMaxTime")]
		public long? PrecipIntensityMaxTime { get; set; }

		/// <summary>
		/// The probability of precipitation occurring, between 0 and 1, inclusive.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "precipProbability")]
		public double? PrecipProbability { get; set; }

		/// <summary>
		/// The type of precipitation occurring at the given time.
		/// <para>(If precipIntensity is zero, then this property will not be defined.)</para>
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "precipType")]
		public PrecipitationType PrecipType { get; set; }

		/// <summary>
		/// The sea-level air pressure in millibars.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "pressure")]
		public double? Pressure { get; set; }

		/// <summary>
		/// A human-readable text summary of this data point.
		/// <para>(This property has millions of possible values, so don’t use it for automated purposes: use the <see cref="Icon"/> property, instead!)</para>
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "summary")]
		public string Summary { get; set; }

		/// <summary>
		/// The UNIX time of when the sun will rise during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "sunriseTime")]
		public long? SunriseTime { get; set; }

		/// <summary>
		/// The UNIX time of when the sun will set during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "sunsetTime")]
		public long? SunsetTime { get; set; }

		/// <summary>
		/// The air temperature in degrees Fahrenheit.
		/// </summary>
		/// <remarks>optional, not on minutely</remarks>
		[JsonProperty(PropertyName = "temperature")]
		public double? Temperature { get; set; }

		/// <summary>
		/// The maximum value of <see cref="Temperature"/> during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "temperatureMax")]
		public double? TemperatureMax { get; set; }

		/// <summary>
		/// The UNIX time of when <see cref="TemperatureMax"/> occurs during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "temperatureMaxTime")]
		public long? TemperatureMaxTime { get; set; }

		/// <summary>
		/// The minimum value of <see cref="Temperature"/> during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "temperatureMin")]
		public double? TemperatureMin { get; set; }

		/// <summary>
		/// The UNIX time of when <see cref="TemperatureMin"/> occurs during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "temperatureMinTime")]
		public long? TemperatureMinTime { get; set; }

		/// <summary>
		/// The UNIX time at which this data point begins. minutely data point are always aligned to the top of the minute, hourly data point objects to the top of the hour, and daily data point objects to midnight of the day, all according to the local time zone.
		/// </summary>
		[JsonProperty(PropertyName = "time")]
		public long Time { get; set; }

		/// <summary>
		/// The UV index.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "uvIndex")]
		public int? UvIndex { get; set; }

		/// <summary>
		/// The UNIX time of when the maximum <see cref="UvIndex"/> occurs during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "uvIndexTime")]
		public int? UvIndexTime { get; set; }

		/// <summary>
		/// The average visibility in miles, capped at 10 miles.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "visibility")]
		public double? Visibility { get; set; }

		/// <summary>
		/// The direction that the wind is coming from in degrees, with true north at 0° and progressing clockwise.
		/// <para>(If <see cref="WindSpeed"/> is zero, then this value will not be defined.)</para>
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "windBearing")]
		public int? WindBearing { get; set; }

		/// <summary>
		/// The wind gust speed in miles per hour.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "windGust")]
		public double? WindGust { get; set; }

		/// <summary>
		/// The UNIX time of when the maximum <see cref="WindGust"/> occurs during a given day.
		/// </summary>
		/// <remarks>optional, only on daily</remarks>
		[JsonProperty(PropertyName = "windGustTime")]
		public long? WindGustTime { get; set; }

		/// <summary>
		/// The wind speed in miles per hour.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "windSpeed")]
		public double? WindSpeed { get; set; }
	}
}