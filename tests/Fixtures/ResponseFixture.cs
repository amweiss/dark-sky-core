namespace DarkSky.Tests.UnitTests.Fixtures
{
    using DarkSky.Models;
    using DarkSky.Services;
    using Moq;
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ResponseFixture
    {
        public ResponseFixture()
        {
            var mockClient = new Mock<IHttpClient>();
            mockClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(MockHttpResponse));

            using (var darkSkyService = new DarkSkyService("fakekey", httpClient: mockClient.Object))
            {
                NormalResponse = darkSkyService.GetForecast(Latitude, Longitude).Result;
            }

            var mockMissingDataCLient = new Mock<IHttpClient>();
            mockMissingDataCLient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(MockHttpResponseMissingData));

            using (var darkSkyServiceMissingData = new DarkSkyService("fakekey", httpClient: mockMissingDataCLient.Object))
            {
                MissingDataResponse = darkSkyServiceMissingData.GetForecast(Latitude, Longitude).Result;
            }
        }

        public static double Latitude => 53.7436;  // 42.915;
        public static double Longitude => -0.3395;  // -78.741;

        public static HttpResponseMessage MockHttpResponse => new HttpResponseMessage
        {
            Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/KuH.json")),
        };

        public static HttpResponseMessage MockHttpResponseMissingData => new HttpResponseMessage
        {
            Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/BuffaloNY_MissingBlocks.json")),
        };

        public DarkSkyResponse MissingDataResponse { get; private set; }
        public DarkSkyResponse NormalResponse { get; private set; }
    }
}