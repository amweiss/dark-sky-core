namespace DarkSkyCore.Tests.UnitTests.Services
{
    using DarkSky.Services;
    using Xunit;

    public class ZipHttpClientUnitTests
    {
        [Fact]
        public void DisposeTest()
        {
            var service = new ZipHttpClient();
            service.Dispose();
            service.Dispose();
        }
    }
}