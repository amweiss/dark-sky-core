using System.Net.Http.Headers;

namespace DarkSky.Models
{
	public class DarkSkyResponse
	{
		public class ResponseHeaders
		{
			public CacheControlHeaderValue CacheControl { get; set; }
            public long? ApiCalls { get; set; }
            public string ResponseTime { get; set; }
		}

		public ResponseHeaders Headers { get; set; }

		public Forecast Response { get; set; }

        public string DataSource => "https://darksky.net/poweredby/";

        public string AttributionLine => "Powered by Dark Sky";
    }
}