namespace DarkSky.Tests.UnitTests.Models
{
	using System;
	using DarkSky.Tests.UnitTests.Fixtures;
	using Xunit;

	public class DataPointUnitTests : IClassFixture<ResponseFixture>
	{
		ResponseFixture _fixture;

		public DataPointUnitTests(ResponseFixture fixture)
		{
			_fixture = fixture;
		}

		[Fact]
		public void ApparentTemperatureHighDateTimeTest()
		{
			var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // Thursday, September 20, 2018 12:00:00 PM UTC

			Assert.Equal(dataPoint.ApparentTemperatureHighDateTime, new DateTime(2018, 9, 20, 12, 0, 0, DateTimeKind.Utc));
		}

		[Fact]
		public void ApparentTemperatureLowDateTimeTest()
		{
			var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // Friday, September 21, 2018 6:00:00 AM UTC

			Assert.Equal(dataPoint.ApparentTemperatureLowDateTime, new DateTime(2018, 9, 21, 6, 0, 0, DateTimeKind.Utc));
		}

		[Fact]
		public void DateTimeTest()
		{
			var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // Wednesday, September 19, 2018 11:00:00 PM UTC

			Assert.Equal(dataPoint.DateTime, new DateTime(2018, 9, 19, 23, 0, 0, DateTimeKind.Utc));
		}

		[Fact]
		public void PrecipIntensityMaxDateTimeTest()
		{
			var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // Thursday, September 20, 2018 3:00:00 PM UTC

			Assert.Equal(dataPoint.PrecipIntensityMaxDateTime, new DateTime(2018, 9, 20, 15, 0, 0, DateTimeKind.Utc));
		}

		[Fact]
		public void SunriseDateTimeTest()
		{
			var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // Thursday, September 20, 2018 5:44:55 AM UTC

			Assert.Equal(dataPoint.SunriseDateTime, new DateTime(2018, 9, 20, 5, 44, 55, DateTimeKind.Utc));
		}

		[Fact]
		public void SunsetDateTimeTest()
		{
			var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // Thursday, September 20, 2018 6:07:18 PM UTC

			Assert.Equal(dataPoint.SunsetDateTime, new DateTime(2018, 9, 20, 18, 07, 18, DateTimeKind.Utc));
		}

		[Fact]
		public void TemperatureHighDateTimeTest()
		{
			var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // Thursday, September 20, 2018 12:00:00 PM UTC

			Assert.Equal(dataPoint.TemperatureHighDateTime, new DateTime(2018, 9, 20, 12, 0, 0, DateTimeKind.Utc));
		}

		[Fact]
		public void TemperatureLowDateTimeTest()
		{
			var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // Friday, September 21, 2018 6:00:00 AM UTC

			Assert.Equal(dataPoint.TemperatureLowDateTime, new DateTime(2018, 9, 21, 6, 0, 0, DateTimeKind.Utc));
		}

		[Fact]
		public void UvIndexDateTimeTest()
		{
			var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // Thursday, September 20, 2018 11:00:00 AM UTC

			Assert.Equal(dataPoint.UvIndexDateTime, new DateTime(2018, 9, 20, 11, 0, 0, DateTimeKind.Utc));
		}

		[Fact]
		public void WindGustDateTimeTest()
		{
			var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // Thursday, September 20, 2018 7:00:00 PM UTC

			Assert.Equal(dataPoint.WindGustDateTime, new DateTime(2018, 9, 20, 19, 0, 0, DateTimeKind.Utc));
		}
	}
}