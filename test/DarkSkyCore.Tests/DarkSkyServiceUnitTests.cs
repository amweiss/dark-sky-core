using Xunit;
using DarkSky.Services;
using System;

namespace DarkSky.UnitTests.Services
{
	public class DarkSkyServiceUnitTests
	{
        public DarkSkyServiceUnitTests()
		{
        }

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("	")]
		[InlineData("  ")]
		public void ExceptionThrownForMissingApiKey(string value)
		{
			Assert.ThrowsAny<ArgumentException>(() =>
			{
				var darkSkyService = new DarkSkyService(value);
				var result = darkSkyService.GetForecast(0, 0);
			}
			);
		}

		[Fact]
		public void ConstructorWithNonEmptyApiKey()
		{
            var darkSkyService = new DarkSkyService("fakekey");
            Assert.NotNull(darkSkyService);
        }
	}
}
