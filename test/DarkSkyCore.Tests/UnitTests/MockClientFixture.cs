namespace DarkSky.UnitTests.Services
{
	using System;
	using System.IO;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;
	using DarkSky.Services;
	using Moq;

	public class MockClientFixture : IDisposable
	{
		public MockClientFixture()
		{
			var cannedJson = File.ReadAllText($"{AppContext.BaseDirectory}/Data/BexarTX.json");
			var mockHttpResponse = new HttpResponseMessage
			{
				Content = new StringContent(cannedJson),
			};
			mockHttpResponse.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = new TimeSpan(0, CacheMinutes, 0) };
			mockHttpResponse.Headers.Add("X-Forecast-API-Calls", ApiCalls.ToString());
			mockHttpResponse.Headers.Add("X-Response-Time", ResponseTime);

			MockClient = new Mock<IHttpClient>();
			MockClient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(mockHttpResponse));
		}

		public int ApiCalls => 10;

		public int CacheMinutes => 1;

		public Mock<IHttpClient> MockClient { get; set; }

		public string ResponseTime => "30ms";

		public void Dispose()
		{
			MockClient = null;
		}
	}
}