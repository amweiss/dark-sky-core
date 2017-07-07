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
			MockClient = new Mock<IHttpClient>();
			MockClient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(GetMockResponse());
		}

		public bool AddApiCallsHeader { get; set; }

		public bool AddResponseTimeHeader { get; set; }

		public int ApiCalls => 10;

		public string ApiCallsResponseText { get; set; }

		public int CacheMinutes => 1;

		public Mock<IHttpClient> MockClient { get; set; }

		public string ResponseTime => "30ms";

		public void Dispose()
		{
			MockClient = null;
		}

		private Func<Task<HttpResponseMessage>> GetMockResponse()
		{
			var cannedJson = File.ReadAllText($"{AppContext.BaseDirectory}/Data/BexarTX.json");

			return new Func<Task<HttpResponseMessage>>(() =>
			{
				var mockHttpResponse = new HttpResponseMessage
				{
					Content = new StringContent(cannedJson),
				};

				mockHttpResponse.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = new TimeSpan(0, CacheMinutes, 0) };

				if (AddApiCallsHeader)
				{
					mockHttpResponse.Headers.Add("X-Forecast-API-Calls", ApiCallsResponseText);
				}

				if (AddResponseTimeHeader)
				{
					mockHttpResponse.Headers.Add("X-Response-Time", ResponseTime);
				}

				return Task.FromResult(mockHttpResponse);
			});
		}
	}
}