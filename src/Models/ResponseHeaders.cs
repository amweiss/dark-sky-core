#region

using System;
using System.Net.Http.Headers;

#endregion

namespace DarkSky.Models
{
    /// <summary>
    ///     The API will set the following HTTP response headers to values useful to developers.
    /// </summary>
    public class ResponseHeaders
    {
        /// <summary>
        ///     The number of API requests made by the given API key for today.
        /// </summary>
        /// <remarks>optional.</remarks>
        public long? ApiCalls { get; set; }

        /// <summary>
        ///     Set to a conservative value for data caching purposes based on the data present in
        ///     the response body.
        /// </summary>
        /// <remarks>optional.</remarks>
        public CacheControlHeaderValue CacheControl { get; set; }

        /// <summary>
        ///     Set to a conservative value for data caching purposes based on the data present in
        ///     the response body.
        /// </summary>
        [Obsolete("Use CacheControl instead.")]
        public DateTimeOffset? Expires { get; set; }

        /// <summary>
        ///     The server-side response time of the request.
        /// </summary>
        /// <remarks>optional.</remarks>
        public string ResponseTime { get; set; }
    }
}