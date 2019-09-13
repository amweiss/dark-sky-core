#region

using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DarkSky.Models;
using DarkSky.Services;
using Moq;

#endregion

namespace DarkSky.Tests.UnitTests.Fixtures
{
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

            var mockMissingDataClient = new Mock<IHttpClient>();
            mockMissingDataClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(MockHttpResponseMissingData));

            using (var darkSkyServiceMissingData =
                new DarkSkyService("fakekey", httpClient: mockMissingDataClient.Object))
            {
                MissingDataResponse = darkSkyServiceMissingData.GetForecast(Latitude, Longitude).Result;
            }

            var invalidKeyClient = new Mock<IHttpClient>();
            invalidKeyClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(MockHttpResponseInvalidKey));

            using (var darkSkyServiceForbidden = new DarkSkyService("fakekey", httpClient: invalidKeyClient.Object))
            {
                ForbiddenResponse = darkSkyServiceForbidden.GetForecast(Latitude, Longitude).Result;
            }

            var badDataClient = new Mock<IHttpClient>();
            badDataClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(MockHttpResponseBadData));

            using (var darkSkyServiceBadData = new DarkSkyService("fakekey", httpClient: badDataClient.Object))
            {
                BadDataResponse = darkSkyServiceBadData.GetForecast(Latitude, Longitude).Result;
            }

            var alertDataClient = new Mock<IHttpClient>();
            alertDataClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(MockHttpResponseAlertData));

            using (var darkSkyServiceAlertData = new DarkSkyService("fakekey", httpClient: alertDataClient.Object))
            {
                AlertResponse = darkSkyServiceAlertData.GetForecast(Latitude, Longitude).Result;
            }
        }

        public static long ApiCalls => 100;
        public static double Latitude => 42.3601;
        public static double Longitude => -71.0589;

        public static string ResponseTime => "0.200ms";

        public DarkSkyResponse AlertResponse { get; }
        public DarkSkyResponse BadDataResponse { get; }
        public DarkSkyResponse MissingDataResponse { get; }
        public DarkSkyResponse NormalResponse { get; }
        public DarkSkyResponse ForbiddenResponse { get; }

        private static HttpResponseMessage MockHttpResponseBadData
        {
            get
            {
                var response = new HttpResponseMessage {Content = new StringContent("Hi there")};
                response.Headers.Add("Cache-Control", "no-store");
                response.Headers.Add("X-Response-Time", "not a time");
                response.Headers.Add("X-Forecast-API-Calls", "cows");
                return response;
            }
        }

        private static HttpResponseMessage MockHttpResponse
        {
            get
            {
                var response = new HttpResponseMessage
                {
                    Content = new StringContent(
                        File.ReadAllText($"{AppContext.BaseDirectory}/Data/Boston_TimeMachine.json"))
                };
                response.Headers.Add("X-Response-Time", ResponseTime);
                response.Headers.Add("X-Forecast-API-Calls", ApiCalls.ToString(CultureInfo.InvariantCulture));
                return response;
            }
        }

        private static HttpResponseMessage MockHttpResponseAlertData
        {
            get
            {
                var response = new HttpResponseMessage
                {
                    Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/KuH.json"))
                };
                response.Headers.Add("Cache-Control", "no-store");
                return response;
            }
        }

        private static HttpResponseMessage MockHttpResponseMissingData
        {
            get
            {
                var response = new HttpResponseMessage
                {
                    Content = new StringContent(
                        File.ReadAllText($"{AppContext.BaseDirectory}/Data/BuffaloNY_MissingBlocks.json"))
                };
                response.Headers.Add("Cache-Control", "no-store");
                return response;
            }
        }

        private static HttpResponseMessage MockHttpResponseInvalidKey
        {
            get
            {
                var response = new HttpResponseMessage
                {
                    Content = new StringContent("{\"code\":403,\"error\":\"permission denied\"}"),
                    StatusCode = HttpStatusCode.Forbidden,
                    ReasonPhrase = "Forbidden"
                };

                return response;
            }
        }
    }
}