namespace DarkSky.Extensions
{
	using System;
	using NodaTime;

	/// <summary>
	/// Extensions for the <see cref="long"/> type.
	/// </summary>
	public static class LongExtensions
	{
		/// <summary>
		/// Convert the UNIX timestamp to a <see cref="DateTimeOffset"/> for the given IANA <paramref name="timezone"/>.
		/// </summary>
		/// <param name="time">A UNIX timestamp</param>
		/// <param name="timezone">An IANA timezone string</param>
		/// <returns>A DateTimeOffset representing the moment in time</returns>
		public static DateTimeOffset ToDateTimeOffsetFromUnixTimestamp(this long time, string timezone)
		{
			var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);
			var instant = Instant.FromDateTimeOffset(dateTimeOffset);
			var zdt = instant.InZone(DateTimeZoneProviders.Tzdb[timezone]);
			return zdt.ToDateTimeOffset();
		}
	}
}
