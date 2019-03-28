namespace DarkSky.Tests.UnitTests.Models
{
	using System;
	using DarkSky.Tests.UnitTests.Fixtures;
	using Xunit;

	public class FlagsUnitTests : IClassFixture<ResponseFixture>
	{
		ResponseFixture _fixture;

		public FlagsUnitTests(ResponseFixture fixture)
		{
			_fixture = fixture;
		}

		[Fact]
		public void FlagsAdditionalDataExistsTest()
		{
			var forecast = _fixture.MissingDataResponse;
			Assert.NotEmpty(forecast.Response.Flags.AdditionalData);
		}
	}
}