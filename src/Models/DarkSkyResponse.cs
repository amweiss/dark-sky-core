namespace DarkSky.Models
{
    /// <summary>
    /// A wapper around the actual Dark Sky response as well as useful properties for interacting
    /// with the response.
    /// </summary>
    public class DarkSkyResponse
    {
        /// <summary>
        /// You agree that any application or service which incorporates data obtained from the
        /// Service shall prominently display the message in this property in a legible manner near
        /// the data or any information derived from any data from the Service.
        /// <para>
        /// This message must, if possible, open a link to <see cref="DataSource"/> when clicked or touched.
        /// </para>
        /// <para>
        /// You may not display or invoke the Service or Dark Sky name or logo in any manner that
        /// implies a relationship or affiliation with, sponsorship, promotion, or endorsement by
        /// Dark Sky, except as authorized by these <a
        /// href="https://darksky.net/dev/docs/terms">Terms of Service</a>.
        /// </para>
        /// </summary>
        public string AttributionLine => "Powered by Dark Sky";

        /// <summary>
        /// The link to use during data source attribution.
        /// </summary>
        public string DataSource => "https://darksky.net/poweredby/";

        /// <summary>
        /// The <see cref="ResponseHeaders"/> for the API request.
        /// </summary>
        public ResponseHeaders Headers { get; set; }

        /// <summary>
        /// The response from the Dark Sky API is a success status.
        /// </summary>
        public bool IsSuccessStatus { get; set; }

        /// <summary>
        /// The API <see cref="Forecast"/> response.
        /// </summary>
        public Forecast Response { get; set; }

        /// <summary>
        /// Dark Sky response ReasonPhrase.
        /// </summary>
        public string ResponseReasonPhrase { get; set; }
    }
}