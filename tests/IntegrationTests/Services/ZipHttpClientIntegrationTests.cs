namespace DarkSkyCore.Tests.IntegrationTests.Services
{
    using DarkSky.Services;
    using Xunit;

    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public sealed class ZipHttpClientIntegrationTests
    {
        [Fact]
        public void DisposalTest()
        {
            new ZipHttpClient().Dispose();
            Assert.True(true);
        }
    }
}