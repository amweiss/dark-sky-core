using Xunit;
using DarkSky.Services;
using System;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DarkSky.UnitTests.Services
{
	public class DarkSkyServiceUnitTests
	{
        Mock<IHttpClient> _mockCLient;

        public DarkSkyServiceUnitTests()
		{
			_mockCLient = new Mock<IHttpClient>();
            _mockCLient.Setup(f => f.HttpRequest(null)).Returns(Task.FromResult(new HttpResponseMessage()
            {
				Content = new StringContent("") //TODO: Canned response
				//TODO: Headers
            }));
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

		[Fact]
		public void GetForecastWithMockData()
		{

        }
	}
}
