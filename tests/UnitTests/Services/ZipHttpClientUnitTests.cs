namespace DarkSkyCore.Tests.UnitTests.Services
{
    using System;
    using DarkSky.Services;
    using Xunit;

    public class ZipHttpClientUnitTests
    {
        [Fact]
        public void DisposeTest()
        {
            using var service = new ZipHttpClient();
            service.Dispose();
        }
    }
}