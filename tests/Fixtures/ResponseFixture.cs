namespace DarkSky.Tests.UnitTests.Fixtures
{
	using System;
	using System.IO;
	using System.Net.Http;
	using System.Threading.Tasks;
	using DarkSky.Models;
	using DarkSky.Services;
	using Moq;

	public class ResponseFixture
	{
		public ResponseFixture()
		{
			var mockClient = new Mock<IHttpClient>();
			mockClient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(MockHttpResponse));

			var darkSkyService = new DarkSkyService("fakekey", mockClient.Object);
			NormalResponse = darkSkyService.GetForecast(Latitude, Longitude).Result;

			var mockMissingDataCLient = new Mock<IHttpClient>();
			mockMissingDataCLient.Setup(f => f.HttpRequest(It.IsAny<string>())).Returns(Task.FromResult(MockHttpResponseMissingData));

			var darkSkyServiceMissingData = new DarkSkyService("fakekey", mockMissingDataCLient.Object);
			MissingDataResponse = darkSkyServiceMissingData.GetForecast(Latitude, Longitude).Result;
		}

		public double Latitude { get => 53.7436; } // 42.915;
		public double Longitude { get => -0.3395; } // -78.741;

		public HttpResponseMessage MockHttpResponse
		{
			get => new HttpResponseMessage
			{
				Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/KuH.json")),
			};
		}

		public HttpResponseMessage MockHttpResponseMissingData
		{
			get => new HttpResponseMessage
			{
				Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/BuffaloNY_MissingBlocks.json")),
			};
		}

		public DarkSkyResponse MissingDataResponse { get; private set; }
		public DarkSkyResponse NormalResponse { get; private set; }
	}
}