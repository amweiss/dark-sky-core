using Xunit;
using DarkSky.Services;
using System;

namespace DarkSky.UnitTests.Services
{
	public class DarkSkyService_GetForecastShould
	{
		private readonly DarkSkyService _darkSkyService;

		public DarkSkyService_GetForecastShould()
		{
			_darkSkyService = new DarkSkyService();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("	")]
		[InlineData("  ")]
		public void ExceptionThrownForMissingKey(string value)
		{
			Assert.ThrowsAny<ArgumentException>(() =>
			{
				var result = _darkSkyService.GetForecast(value, 0, 0);
			}
			);
		}
	}
}
