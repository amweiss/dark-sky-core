namespace DarkSkyCore.Tests.UnitTests.Models
{
    #region

    using Fixtures;
    using Xunit;

    #endregion

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