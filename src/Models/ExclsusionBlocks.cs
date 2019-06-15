namespace DarkSky.Models
{
    /// <summary>
    /// Values to indicate which <see cref="DataBlock"/> elements to exclude.
    /// </summary>
    public enum ExclusionBlock
    {
        /// <summary>
        /// Unknown exclusion.
        /// </summary>
        None = 0,

        /// <summary>
        /// Currently.
        /// </summary>
        Currently = 1 << 1,

        /// <summary>
        /// Minutely.
        /// </summary>
        Minutely = 1 << 2,

        /// <summary>
        /// Hourly.
        /// </summary>
        Hourly = 1 << 3,

        /// <summary>
        /// Daily.
        /// </summary>
        Daily = 1 << 4,

        /// <summary>
        /// Alerts.
        /// </summary>
        Alerts = 1 << 5,

        /// <summary>
        /// Flags.
        /// </summary>
        Flags = 1 << 6,
    }
}