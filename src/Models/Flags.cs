namespace DarkSky.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// The flags object contains various metadata information related to the request.
    /// </summary>
    public class Flags
    {
        /// <summary>
        /// Additional unmapped flags end up here.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalData { get; set; }

        /// <summary>
        /// The presence of this property indicates that the Dark Sky data source supports the given
        /// location, but a temporary error (such as a radar station being down for maintenance) has
        /// made the data unavailable.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "darksky-unavailable")]
        public string DarkskyUnavailable { get; set; }

        /// <summary>
        /// Undocumented.
        /// </summary>
        [JsonProperty(PropertyName = "nearest-station")]
        public double? NearestStation { get; set; }

        /// <summary>
        /// This property contains an array of IDs for each <a
        /// href="https://darksky.net/dev/docs/sources">data source</a> utilized in servicing this request.
        /// </summary>
        [JsonProperty(PropertyName = "sources")]
        public List<string> Sources { get; set; }

        /// <summary>
        /// Indicates the units which were used for the data in this request.
        /// </summary>
        [JsonProperty(PropertyName = "units")]
        public string Units { get; set; }
    }
}