#region

using System;
using NodaTime;

#endregion

namespace DarkSky.Extensions
{
    /// <summary>
    ///     Extensions for the <see cref="long" /> type.
    /// </summary>
    public static class LongExtensions
    {
        /// <summary>
        ///     Convert the UNIX timestamp to a <see cref="DateTimeOffset" /> for the given IANA <paramref name="timezone" />.
        /// </summary>
        /// <param name="time">A UNIX timestamp.</param>
        /// <param name="timezone">An IANA timezone string.</param>
        /// <returns>A DateTimeOffset representing the moment in time.</returns>
        public static DateTimeOffset ToDateTimeOffsetFromUnixTimestamp(this long time, string timezone)
        {
            var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);
            var instant = Instant.FromDateTimeOffset(dateTimeOffset);
            var zdt = string.IsNullOrWhiteSpace(timezone)
                ? instant.InUtc()
                : instant.InZone(DateTimeZoneProviders.Tzdb[timezone]);
            return zdt.ToDateTimeOffset();
        }
    }
}