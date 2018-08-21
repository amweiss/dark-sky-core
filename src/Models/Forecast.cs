namespace DarkSky.Models
{
	using System;
	using System.Collections.Generic;
	using Newtonsoft.Json;

	/// <summary>
	/// API responses consist of a UTF-8-encoded, JSON-formatted object that this class wraps.
	/// </summary>
	public class Forecast
	{
		/// <summary>
		/// An <see cref="Alert"/> list, which, if present, contains any severe weather alerts
		/// pertinent to the requested location.
		/// </summary>
		/// <remarks>optional.</remarks>
		[JsonProperty(PropertyName = "alerts")]
		public List<Alert> Alerts { get; set; }

		/// <summary>
		/// A <see cref="DataPoint"/> containing the current weather conditions at the requested location.
		/// </summary>
		/// <remarks>optional.</remarks>
		[JsonProperty(PropertyName = "currently")]
		public DataPoint Currently { get; set; }

		/// <summary>
		/// A <see cref="DataBlock"/> containing the weather conditions day-by-day for the next week.
		/// </summary>
		/// <remarks>optional.</remarks>
		[JsonProperty(PropertyName = "daily")]
		public DataBlock Daily { get; set; }

		/// <summary>
		/// A <see cref="Models.Flags"/> containing miscellaneous metadata about the request.
		/// </summary>
		/// <remarks>optional.</remarks>
		[JsonProperty(PropertyName = "flags")]
		public Flags Flags { get; set; }

		/// <summary>
		/// A <see cref="DataBlock"/> containing the weather conditions hour-by-hour for the next two days.
		/// </summary>
		/// <remarks>optional.</remarks>
		[JsonProperty(PropertyName = "hourly")]
		public DataBlock Hourly { get; set; }

		/// <summary>
		/// The requested latitude.
		/// </summary>
		[JsonProperty(PropertyName = "latitude")]
		public double Latitude { get; set; }

		/// <summary>
		/// The requested longitude.
		/// </summary>
		[JsonProperty(PropertyName = "longitude")]
		public double Longitude { get; set; }

		/// <summary>
		/// A <see cref="DataBlock"/> containing the weather conditions minute-by-minute for the next hour.
		/// </summary>
		/// <remarks>optional.</remarks>
		[JsonProperty(PropertyName = "minutely")]
		public DataBlock Minutely { get; set; }

		/// <summary>
		/// The current timezone offset in hours. (Use of this property will almost certainly result
		/// in Daylight Saving Time bugs. Please use <see cref="TimeZone"/>, instead).
		/// </summary>
		[Obsolete("Use of this property will almost certainly result in Daylight Saving Time bugs. Please use timezone, instead.")]
		[JsonProperty(PropertyName = "offset")]
		public string Offset { get; set; }

		/// <summary>
		/// The IANA timezone name for the requested location. This is used for text summaries and
		/// for determining when <see cref="Hourly"/> and <see cref="Daily"/><see cref="DataBlock"/>
		/// objects begin.
		/// </summary>
		[JsonProperty(PropertyName = "timezone")]
		public string TimeZone { get; set; }
	}
}