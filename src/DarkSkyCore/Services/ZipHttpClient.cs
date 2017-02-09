namespace DarkSky.Services
{
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Threading.Tasks;

	public class ZipHttpClient : IHttpClient
	{
		readonly string _baseUri = string.Empty;

		public ZipHttpClient(string baseUri)
		{
			_baseUri = baseUri;
		}

		public async Task<HttpResponseMessage> HttpRequest(string requestString)
		{
			using (var handler = new HttpClientHandler())
			{
				if (handler.SupportsAutomaticDecompression)
				{
					handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
				}

				using (var client = new HttpClient(handler))
				{
					client.BaseAddress = new Uri(_baseUri);
					return await client.GetAsync(requestString);
				}
			}
		}
	}
}