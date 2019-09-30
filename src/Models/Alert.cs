namespace DarkSky.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using static Extensions.LongExtensions;

    /// <summary>
    ///     The alerts array contains objects representing the severe weather warnings issued for the
    ///     requested location by a governmental authority (please see our
    ///     <a
    ///         href="https://darksky.net/dev/docs/sources">
    ///         data sources page
    ///     </a>
    ///     for a list of sources).
    /// </summary>
    public class Alert
    {
        /// <summary>
        ///     The time at which the alert was issued.
        /// </summary>
        public DateTimeOffset DateTime => UnixTime.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     A detailed description of the alert.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     The time at which the alert will expire. (Some alerts sources, unfortunately, do not
        ///     define expiration time, and in these cases this parameter will not be defined).
        /// </summary>
        public DateTimeOffset ExpiresDateTime => UnixExpires.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     A <see cref="List{T}" /> of strings representing the names of the regions covered by this
        ///     weather alert.
        /// </summary>
        [JsonProperty(PropertyName = "regions")]
        public List<string> Regions { get; set; }

        /// <summary>
        ///     The severity of the weather alert. Will take one of the following values: "advisory" (an
        ///     individual should be aware of potentially severe weather), "watch" (an individual should
        ///     prepare for potentially severe weather), or "warning" (an individual should take
        ///     immediate action to protect themselves and others from potentially severe weather).
        /// </summary>
        [JsonProperty(PropertyName = "severity")]
        public string Severity { get; set; }

        /// <summary>
        ///     A brief description of the alert.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        ///     An HTTP(S) URI that one may refer to for detailed information about the alert.
        /// </summary>
        [JsonProperty(PropertyName = "uri")]
        public Uri Uri { get; set; }

        /// <summary>
        ///     TimeZone from the parent Forecast object.
        /// </summary>
        internal string TimeZone { get; set; }

        /// <summary>
        ///     The UNIX time at which the alert will expire. (Some alerts sources, unfortunately, do not
        ///     define expiration time, and in these cases this parameter will not be defined).
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "expires")]
        internal long UnixExpires { get; set; }

        /// <summary>
        ///     The UNIX time at which the alert was issued.
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        internal long UnixTime { get; set; }
    }
}