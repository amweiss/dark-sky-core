﻿#region

using System;
using DarkSky.Extensions;
using Newtonsoft.Json;

#endregion

namespace DarkSky.Models
{
    #region

    using static LongExtensions;

    #endregion

    /// <summary>
    ///     A data point object contains various properties, each representing the average (unless
    ///     otherwise specified) of a particular weather phenomenon occurring during a period of time: an
    ///     instant in the case of <see cref="Forecast.Currently" />, a minute for
    ///     <see
    ///         cref="Forecast.Minutely" />
    ///     , an hour for <see cref="Forecast.Hourly" />, and a day for <see cref="Forecast.Daily" />.
    /// </summary>
    public class DataPoint
    {
        /// <summary>
        ///     The apparent (or “feels like”) temperature in degrees Fahrenheit.
        /// </summary>
        /// <remarks>optional, not on daily.</remarks>
        [JsonProperty(PropertyName = "apparentTemperature")]
        public double? ApparentTemperature { get; set; }

        /// <summary>
        ///     The daytime high apparent temperature.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "apparentTemperatureHigh")]
        public double? ApparentTemperatureHigh { get; set; }

        /// <summary>
        ///     The time of when <see cref="ApparentTemperatureHigh" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        public DateTimeOffset? ApparentTemperatureHighDateTime =>
            ApparentTemperatureHighTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The overnight low apparent temperature.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "apparentTemperatureLow")]
        public double? ApparentTemperatureLow { get; set; }

        /// <summary>
        ///     The time of when <see cref="ApparentTemperatureLow" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        public DateTimeOffset? ApparentTemperatureLowDateTime =>
            ApparentTemperatureLowTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The maximum value of <see cref="ApparentTemperature" /> during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use apparentTemperatureHigh instead.")]
        [JsonProperty(PropertyName = "apparentTemperatureMax")]
        public double? ApparentTemperatureMax { get; set; }

        /// <summary>
        ///     The time of when <see cref="ApparentTemperatureMax" /> occurs during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use apparentTemperatureHighDateTime instead.")]
        public DateTimeOffset? ApparentTemperatureMaxDateTime =>
            ApparentTemperatureMaxTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The minimum value of <see cref="ApparentTemperature" /> during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use apparentTemperatureLow instead.")]
        [JsonProperty(PropertyName = "apparentTemperatureMin")]
        public double? ApparentTemperatureMin { get; set; }

        /// <summary>
        ///     The time of when <see cref="ApparentTemperatureMin" /> occurs during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use apparentTemperatureLowDateTime instead.")]
        public DateTimeOffset? ApparentTemperatureMinDateTime =>
            ApparentTemperatureMinTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The percentage of sky occluded by clouds, between 0 and 1, inclusive.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "cloudCover")]
        public double? CloudCover { get; set; }

        /// <summary>
        ///     The time at which this data point begins. minutely data point are always aligned to the
        ///     top of the minute, hourly data point objects to the top of the hour, and daily data point
        ///     objects to midnight of the day, all according to the local time zone.
        /// </summary>
        public DateTimeOffset DateTime => TimeUnix.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The dew point in degrees Fahrenheit.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "dewPoint")]
        public double? DewPoint { get; set; }

        /// <summary>
        ///     The relative humidity, between 0 and 1, inclusive.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "humidity")]
        public double? Humidity { get; set; }

        /// <summary>
        ///     A machine-readable text summary of this data point, suitable for selecting an icon for display.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "icon")]
        public Icon Icon { get; set; }

        /// <summary>
        ///     The fractional part of the lunation number during the given day: a value of 0 corresponds
        ///     to a new moon, 0.25 to a first quarter moon, 0.5 to a full moon, and 0.75 to a last
        ///     quarter moon.
        ///     <para>
        ///         (The ranges in between these represent waxing crescent, waxing gibbous, waning gibbous,
        ///         and waning crescent moons, respectively).
        ///     </para>
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "moonPhase")]
        public double? MoonPhase { get; set; }

        /// <summary>
        ///     The approximate direction of the nearest storm in degrees, with true north at 0° and
        ///     progressing clockwise.
        ///     <para>(If <see cref="NearestStormDistance" /> is zero, then this value will not be defined).</para>
        /// </summary>
        /// <remarks>optional, only on currently.</remarks>
        [JsonProperty(PropertyName = "nearestStormBearing")]
        public int? NearestStormBearing { get; set; }

        /// <summary>
        ///     The approximate distance to the nearest storm in miles.
        ///     <para>
        ///         (A storm distance of 0 doesn’t necessarily refer to a storm at the requested location,
        ///         but rather a storm in the vicinity of that location).
        ///     </para>
        /// </summary>
        /// <remarks>optional, only on currently.</remarks>
        [JsonProperty(PropertyName = "nearestStormDistance")]
        public double? NearestStormDistance { get; set; }

        /// <summary>
        ///     The columnar density of total atmospheric ozone at the given time in Dobson units.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "ozone")]
        public double? Ozone { get; set; }

        /// <summary>
        ///     The amount of snowfall accumulation expected to occur, in inches.
        ///     <para>(If no snowfall is expected, this property will not be defined).</para>
        /// </summary>
        /// <remarks>optional, only on hourly and daily.</remarks>
        [JsonProperty(PropertyName = "precipAccumulation")]
        public double? PrecipAccumulation { get; set; }

        /// <summary>
        ///     The intensity (in inches of liquid water per hour) of precipitation occurring at the
        ///     given time. This value is conditional on probability (that is, assuming any precipitation
        ///     occurs at all) for minutely data points, and unconditional otherwise.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "precipIntensity")]
        public double? PrecipIntensity { get; set; }

        /// <summary>
        ///     Undocumented, presumably was a value representing the potential error in the <see cref="PrecipIntensity" />.
        /// </summary>
        /// <remarks>deprecated.</remarks>
        [Obsolete("PrecipIntensityError is no longer provided by the DarkSky API.")]
        [JsonProperty(PropertyName = "precipIntensityError")]
        public double? PrecipIntensityError { get; set; }

        /// <summary>
        ///     The maximum value of <see cref="PrecipIntensity" /> during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "precipIntensityMax")]
        public double? PrecipIntensityMax { get; set; }

        /// <summary>
        ///     The time of when <see cref="PrecipIntensityMax" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        public DateTimeOffset? PrecipIntensityMaxDateTime =>
            PrecipIntensityMaxTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The probability of precipitation occurring, between 0 and 1, inclusive.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "precipProbability")]
        public double? PrecipProbability { get; set; }

        /// <summary>
        ///     The type of precipitation occurring at the given time.
        ///     <para>(If precipIntensity is zero, then this property will not be defined).</para>
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "precipType")]
        public PrecipitationType PrecipType { get; set; }

        /// <summary>
        ///     The sea-level air pressure in millibars.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "pressure")]
        public double? Pressure { get; set; }

        /// <summary>
        ///     A human-readable text summary of this data point.
        ///     <para>
        ///         (This property has millions of possible values, so don’t use it for automated purposes:
        ///         use the <see cref="Icon" /> property, instead!).
        ///     </para>
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        /// <summary>
        ///     The time of when the sun will rise during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        public DateTimeOffset? SunriseDateTime => SunriseTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The time of when the sun will set during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        public DateTimeOffset? SunsetDateTime => SunsetTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The air temperature in degrees Fahrenheit.
        /// </summary>
        /// <remarks>optional, not on minutely.</remarks>
        [JsonProperty(PropertyName = "temperature")]
        public double? Temperature { get; set; }

        /// <summary>
        ///     The daytime high temperature.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "temperatureHigh")]
        public double? TemperatureHigh { get; set; }

        /// <summary>
        ///     The time of when <see cref="TemperatureHigh" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        public DateTimeOffset? TemperatureHighDateTime =>
            TemperatureHighTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The overnight low temperature.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "temperatureLow")]
        public double? TemperatureLow { get; set; }

        /// <summary>
        ///     The time of when <see cref="TemperatureLow" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        public DateTimeOffset? TemperatureLowDateTime =>
            TemperatureLowTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The maximum value of <see cref="Temperature" /> during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use temperatureHigh instead.")]
        [JsonProperty(PropertyName = "temperatureMax")]
        public double? TemperatureMax { get; set; }

        /// <summary>
        ///     The time of when <see cref="TemperatureMax" /> occurs during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use temperatureHighDateTime instead.")]
        public DateTimeOffset? TemperatureMaxDateTime =>
            TemperatureMaxTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The minimum value of <see cref="Temperature" /> during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use temperatureLow instead.")]
        [JsonProperty(PropertyName = "temperatureMin")]
        public double? TemperatureMin { get; set; }

        /// <summary>
        ///     The time of when <see cref="TemperatureMin" /> occurs during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use temperatureLowDateTime instead.")]
        public DateTimeOffset? TemperatureMinDateTime =>
            TemperatureMinTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The UV index.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "uvIndex")]
        public int? UvIndex { get; set; }

        /// <summary>
        ///     The time of when the maximum <see cref="UvIndex" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        public DateTimeOffset? UvIndexDateTime => UvIndexTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The average visibility in miles, capped at 10 miles.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "visibility")]
        public double? Visibility { get; set; }

        /// <summary>
        ///     The direction that the wind is coming from in degrees, with true north at 0° and
        ///     progressing clockwise.
        ///     <para>(If <see cref="WindSpeed" /> is zero, then this value will not be defined).</para>
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "windBearing")]
        public int? WindBearing { get; set; }

        /// <summary>
        ///     The wind gust speed in miles per hour.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "windGust")]
        public double? WindGust { get; set; }

        /// <summary>
        ///     The time of when the maximum <see cref="WindGust" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        public DateTimeOffset? WindGustDateTime => WindGustTimeUnix?.ToDateTimeOffsetFromUnixTimestamp(TimeZone);

        /// <summary>
        ///     The wind speed in miles per hour.
        /// </summary>
        /// <remarks>optional.</remarks>
        [JsonProperty(PropertyName = "windSpeed")]
        public double? WindSpeed { get; set; }

        /// <summary>
        ///     The UNIX time of when <see cref="ApparentTemperatureHigh" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "apparentTemperatureHighTime")]
        internal long? ApparentTemperatureHighTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time of when <see cref="ApparentTemperatureLow" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "apparentTemperatureLowTime")]
        internal long? ApparentTemperatureLowTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time of when <see cref="ApparentTemperatureMax" /> occurs during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use apparentTemperatureHighTimeUnix instead.")]
        [JsonProperty(PropertyName = "apparentTemperatureMaxTime")]
        internal long? ApparentTemperatureMaxTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time of when <see cref="ApparentTemperatureMin" /> occurs during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use apparentTemperatureLowTimeUnix instead.")]
        [JsonProperty(PropertyName = "apparentTemperatureMinTime")]
        internal long? ApparentTemperatureMinTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time of when <see cref="PrecipIntensityMax" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "precipIntensityMaxTime")]
        internal long? PrecipIntensityMaxTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time of when the sun will rise during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "sunriseTime")]
        internal long? SunriseTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time of when the sun will set during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "sunsetTime")]
        internal long? SunsetTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time of when <see cref="TemperatureHigh" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "temperatureHighTime")]
        internal long? TemperatureHighTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time of when <see cref="TemperatureLow" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "temperatureLowTime")]
        internal long? TemperatureLowTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time of when <see cref="TemperatureMax" /> occurs during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use temperatureHighTimeUnix instead.")]
        [JsonProperty(PropertyName = "temperatureMaxTime")]
        internal long? TemperatureMaxTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time of when <see cref="TemperatureMin" /> occurs during a given day.
        /// </summary>
        /// <remarks>deprecated, optional, only on daily.</remarks>
        [Obsolete("Use temperatureLowTimeUnix instead.")]
        [JsonProperty(PropertyName = "temperatureMinTime")]
        internal long? TemperatureMinTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time at which this data point begins. minutely data point are always aligned to
        ///     the top of the minute, hourly data point objects to the top of the hour, and daily data
        ///     point objects to midnight of the day, all according to the local time zone.
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        internal long TimeUnix { get; set; }

        /// <summary>
        ///     TimeZone from the parent Forecast object.
        /// </summary>
        internal string TimeZone { get; set; }

        /// <summary>
        ///     The UNIX time of when the maximum <see cref="UvIndex" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "uvIndexTime")]
        internal long? UvIndexTimeUnix { get; set; }

        /// <summary>
        ///     The UNIX time of when the maximum <see cref="WindGust" /> occurs during a given day.
        /// </summary>
        /// <remarks>optional, only on daily.</remarks>
        [JsonProperty(PropertyName = "windGustTime")]
        internal long? WindGustTimeUnix { get; set; }
    }
}