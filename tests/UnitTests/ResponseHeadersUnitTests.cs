namespace DarkSky.Tests.UnitTests
{
    using DarkSky.Services;
    using DarkSky.Tests.UnitTests.Fixtures;
    using Moq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public class ResponseHeadersUnitTests : IClassFixture<ResponseFixture>
    {
        private readonly ResponseFixture _fixture;

        public ResponseHeadersUnitTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ApiCallsHeaderBadValueTest()
        {
            var forecast = _fixture.BadDataResponse;

            // Check Headers (match pre-defined values)
            Assert.NotNull(forecast.Headers);
            Assert.Null(forecast.Headers.ApiCalls);
        }

        [Fact]
        public void ApiCallsHeaderMissingTest()
        {
            var forecast = _fixture.MissingDataResponse;

            // Check Headers (match pre-defined values)
            Assert.NotNull(forecast.Headers);
            Assert.Null(forecast.Headers.ApiCalls);
        }

        [Fact]
        public void ApiCallsHeaderNullValueTest()
        {
            var forecast = _fixture.MissingDataResponse;

            // Check Headers (match pre-defined values)
            Assert.NotNull(forecast.Headers);
            Assert.Null(forecast.Headers.ApiCalls);
        }

        [Fact]
        public void ApiCallsHeaderValidTest()
        {
            var forecast = _fixture.NormalResponse;

            // Check Headers (match pre-defined values)
            Assert.NotNull(forecast.Headers);
            Assert.Equal(ResponseFixture.ApiCalls, forecast.Headers.ApiCalls);
        }
    }
}
