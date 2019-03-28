namespace DarkSky.Tests.UnitTests.Models
{
	using DarkSky.Models;
	using Xunit;

	public class DarkSkyResponseUnitTests
	{
		readonly DarkSkyResponse _model = new DarkSkyResponse();

		[Fact]
		public void RequiredToSData()
		{
			Assert.False(string.IsNullOrWhiteSpace(_model.AttributionLine));
			Assert.False(string.IsNullOrWhiteSpace(_model.DataSource));
		}
	}
}