#region

using System;
using DarkSky.Tests.UnitTests.Fixtures;
using Xunit;

#endregion

namespace DarkSky.Tests.UnitTests.Models
{
    public class DataPointUnitTests : IClassFixture<ResponseFixture>
    {
        public DataPointUnitTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ResponseFixture _fixture;

        [Fact]
        public void ApparentTemperatureHighDateTimeTest()
        {
            var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // 1978-02-06T11:00:00.0000000-05:00

            Assert.Equal(new DateTime(1978, 2, 6, 16, 0, 0, DateTimeKind.Utc),
                dataPoint.ApparentTemperatureHighDateTime);
        }

        [Fact]
        public void ApparentTemperatureLowDateTimeTest()
        {
            var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // 1978-02-06T22:00:00.0000000-05:00

            Assert.Equal(new DateTime(1978, 2, 7, 3, 0, 0, DateTimeKind.Utc), dataPoint.ApparentTemperatureLowDateTime);
        }

        [Fact]
        public void DateTimeTest()
        {
            var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // 1978-02-06T00:00:00.0000000-05:00

            Assert.Equal(new DateTime(1978, 2, 6, 5, 0, 0, DateTimeKind.Utc), dataPoint.DateTime);
        }

        [Fact]
        public void PrecipIntensityMaxDateTimeTest()
        {
            var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // 1978-02-06T19:00:00.0000000-05:00

            Assert.Equal(new DateTime(1978, 2, 7, 0, 0, 0, DateTimeKind.Utc), dataPoint.PrecipIntensityMaxDateTime);
        }

        [Fact]
        public void SunriseDateTimeTest()
        {
            var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // 1978-02-06T06:53:45.0000000-05:00

            Assert.Equal(new DateTime(1978, 2, 6, 11, 53, 45, DateTimeKind.Utc), dataPoint.SunriseDateTime);
        }

        [Fact]
        public void SunsetDateTimeTest()
        {
            var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // 1978-02-06T17:05:38.0000000-05:00

            Assert.Equal(new DateTime(1978, 2, 6, 22, 5, 38, DateTimeKind.Utc), dataPoint.SunsetDateTime);
        }

        [Fact]
        public void TemperatureHighDateTimeTest()
        {
            var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // 1978-02-06T11:00:00.0000000-05:00

            Assert.Equal(new DateTime(1978, 2, 6, 16, 0, 0, DateTimeKind.Utc), dataPoint.TemperatureHighDateTime);
        }

        [Fact]
        public void TemperatureLowDateTimeTest()
        {
            var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // 1978-02-07T06:00:00.0000000-05:00

            Assert.Equal(new DateTime(1978, 2, 7, 11, 0, 0, DateTimeKind.Utc), dataPoint.TemperatureLowDateTime);
        }

        [Fact]
        public void UvIndexDateTimeTest()
        {
            var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // 1978-02-06T12:00:00.0000000-05:00

            Assert.Equal(new DateTime(1978, 2, 6, 17, 0, 0, DateTimeKind.Utc), dataPoint.UvIndexDateTime);
        }

        [Fact]
        public void WindGustDateTimeTest()
        {
            var dataPoint = _fixture.NormalResponse.Response.Daily.Data[0]; // 1978-02-06T22:00:00.0000000-05:00

            Assert.Equal(new DateTime(1978, 2, 7, 3, 0, 0, DateTimeKind.Utc), dataPoint.WindGustDateTime);
        }
    }
}