namespace DarkSkyCore.Tests.UnitTests.Models
{
    #region

    using DarkSky.Models;
    using Xunit;

    #endregion

    public class DarkSkyResponseUnitTests
    {
        private readonly DarkSkyResponse _model = new DarkSkyResponse();

        [Fact]
        public void RequiredToSData()
        {
            Assert.False(string.IsNullOrWhiteSpace(_model.AttributionLine));
            Assert.False(string.IsNullOrWhiteSpace(_model.DataSource));
        }
    }
}