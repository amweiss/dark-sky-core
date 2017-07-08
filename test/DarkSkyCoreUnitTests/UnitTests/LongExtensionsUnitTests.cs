namespace DarkSky.UnitTests.Extensions
{
	using System;
	using NodaTime;
	using Xunit;
	using static DarkSky.Extensions.LongExtensions;

	public class LongExtensionsUnitTests
	{
		public static System.Collections.Generic.IEnumerable<object[]> GetDateTimeOffsets()
		{
			yield return new object[] { DateTimeOffset.MinValue, "UTC" };
			yield return new object[] { DateTimeOffset.MaxValue, "UTC" };
			yield return new object[] { new DateTimeOffset(2017, 1, 1, 1, 0, 0, new TimeSpan(0)), "UTC" };
			yield return new object[] { new ZonedDateTime(Instant.FromUnixTimeSeconds(1499435235), DateTimeZoneProviders.Tzdb["America/New_York"]).ToDateTimeOffset(), "America/New_York" };
		}

		[Theory]
		[MemberData(nameof(GetDateTimeOffsets))]
		public void CorrectConversionTest(DateTimeOffset date, string timezone)
		{
			// Truncate milliseconds since we don't use them for the UNIX timestamps.
			var dateTimeOffset = date.AddTicks(-(date.Ticks % TimeSpan.TicksPerSecond));

			var convertedDate = dateTimeOffset.ToUnixTimeSeconds().ToDateTimeOffsetFromUnixTimestamp(timezone);

			Assert.Equal(dateTimeOffset, convertedDate);
		}
	}
}