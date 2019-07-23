#region

using DarkSky.Tests.UnitTests.Fixtures;
using Xunit;

#endregion

namespace DarkSky.Tests.UnitTests.Models
{
    public class FlagsUnitTests : IClassFixture<ResponseFixture>
    {
        public FlagsUnitTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ResponseFixture _fixture;

        [Fact]
        public void FlagsAdditionalDataExistsTest()
        {
            var forecast = _fixture.MissingDataResponse;
            Assert.NotEmpty(forecast.Response.Flags.AdditionalData);
        }
    }
}