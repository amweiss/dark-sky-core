#region

using System.Runtime.Serialization;
using DarkSky.Services;
using Newtonsoft.Json;

#endregion

namespace DarkSky.Models
{
    /// <summary>
    ///     A machine-readable text summary of this data point, suitable for selecting an icon for display.
    ///     <para>
    ///         (Developers should ensure that a sensible default is defined, as additional values, such as
    ///         hail, thunderstorm, or tornado, may be defined in the future).
    ///     </para>
    /// </summary>
    [JsonConverter(typeof(DarkSkyEnumJsonConverter))]
    public enum Icon
    {
        /// <summary>
        ///     An unknown icon.
        /// </summary>
        [EnumMember(Value = null)] None,

        /// <summary>
        ///     Clear Day.
        /// </summary>
        [EnumMember(Value = "clear-day")] ClearDay,

        /// <summary>
        ///     Clear Night.
        /// </summary>
        [EnumMember(Value = "clear-night")] ClearNight,

        /// <summary>
        ///     Rain.
        /// </summary>
        [EnumMember(Value = "rain")] Rain,

        /// <summary>
        ///     Snow.
        /// </summary>
        [EnumMember(Value = "snow")] Snow,

        /// <summary>
        ///     Sleet.
        /// </summary>
        [EnumMember(Value = "sleet")] Sleet,

        /// <summary>
        ///     Wind.
        /// </summary>
        [EnumMember(Value = "wind")] Wind,

        /// <summary>
        ///     Fog.
        /// </summary>
        [EnumMember(Value = "fog")] Fog,

        /// <summary>
        ///     Cloudy.
        /// </summary>
        [EnumMember(Value = "cloudy")] Cloudy,

        /// <summary>
        ///     Partly Cloudy Day.
        /// </summary>
        [EnumMember(Value = "partly-cloudy-day")]
        PartlyCloudyDay,

        /// <summary>
        ///     Partly Cloudy Night.
        /// </summary>
        [EnumMember(Value = "partly-cloudy-night")]
        PartlyCloudyNight
    }
}