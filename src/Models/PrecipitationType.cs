namespace DarkSky.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Services;

    /// <summary>
    ///     Types of precipitation Dark Sky API can return.
    /// </summary>
    [JsonConverter(typeof(DarkSkyEnumJsonConverter))]
    public enum PrecipitationType
    {
        /// <summary>
        ///     An unknown precipitation.
        /// </summary>
        [EnumMember(Value = null)] None,

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
        ///     <para>(which refers to each of freezing rain, ice pellets, and “wintery mix”)</para>
        /// </summary>
        [EnumMember(Value = "sleet")] Sleet
    }
}