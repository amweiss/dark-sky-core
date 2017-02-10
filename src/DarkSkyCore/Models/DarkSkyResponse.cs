using System.Net.Http.Headers;

namespace DarkSky.Models
{
	public class DarkSkyResponse
	{
		public string AttributionLine => "Powered by Dark Sky";

		public string DataSource => "https://darksky.net/poweredby/";

		public ResponseHeaders Headers { get; set; }

		public Forecast Response { get; set; }

		/// <summary>
		/// Call to DarkSky webservice is successful
		/// </summary>
		public bool IsSuccessful { get; set; }

		/// <summary>
		/// DarkSky response ReasonPhrase text
		/// </summary>
		public string ResponseReasonPhrase { get; set; }

		public class ResponseHeaders
		{
			public long? ApiCalls { get; set; }
			public CacheControlHeaderValue CacheControl { get; set; }
			public string ResponseTime { get; set; }
		}
	}
}