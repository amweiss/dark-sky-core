#region

using System;
using System.Collections.Generic;

#endregion

namespace DarkSky.Models
{
    /// <summary>
    ///     Optional parameters that an be used to modify the API request.
    /// </summary>
    public class OptionalParameters
    {
        /// <summary>
        ///     A List of <see cref="ExclusionBlocks" /> that prevent specific <see cref="DataBlock" />
        ///     properties from being populated from the API.
        /// </summary>
        public List<ExclusionBlocks> DataBlocksToExclude { get; set; }

        /// <summary>
        ///     When present, return hour-by-hour data for the next 168 hours, instead of the next 48.
        ///     <para>When using this option, we strongly recommend enabling HTTP compression.</para>
        /// </summary>
        public bool? ExtendHourly { get; set; }

        /// <summary>
        ///     A Time Machine Request returns the observed (in the past) or forecasted (in the
        ///     future) hour-by-hour weather and daily weather conditions for a particular date.
        ///     <para>
        ///         A Time Machine request is identical in structure to a <see cref="Forecast" />, except:
        ///     </para>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>
        ///                 The currently data point will refer to the time provided, rather than the current time.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 The minutely data block will be omitted, unless you are requesting a time within an
        ///                 hour of the present.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 The hourly data block will contain data points starting at midnight (local time) of
        ///                 the day requested, and continuing until midnight (local time) of the following day.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 The daily data block will contain a single data point referring to the requested date.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>The alerts data block will be omitted.</description>
        ///         </item>
        ///     </list>
        /// </summary>
        public DateTime? ForecastDateTime { get; set; }

        /// <summary>
        ///     Return <see cref="DataBlock.Summary" /> properties in the desired language.
        ///     <para>
        ///         (Note that units in the summary will be set according to the
        ///         <see
        ///             cref="MeasurementUnits" />
        ///         parameter, so be sure to set both parameters appropriately.).
        ///     </para>
        ///     <para>
        ///         English is the default, but see the
        ///         <a
        ///             href="https://darksky.net/dev/docs/forecast">
        ///             forecast documentation page
        ///         </a>
        ///         for
        ///         supported languages.
        ///     </para>
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        ///     Return weather conditions in the requested units.
        ///     <para>
        ///         US Imperial Units are the default, but see the
        ///         <a
        ///             href="https://darksky.net/dev/docs/forecast">
        ///             forecast documentation page
        ///         </a>
        ///         for
        ///         supported units.
        ///     </para>
        /// </summary>
        public string MeasurementUnits { get; set; }
    }
}