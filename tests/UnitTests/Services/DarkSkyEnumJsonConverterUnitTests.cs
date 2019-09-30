namespace DarkSkyCore.Tests.UnitTests.Services
{
    #region

    using DarkSky.Models;
    using Newtonsoft.Json;
    using Xunit;

    #endregion

    public class DarkSkyEnumJsonConverterUnitTests
    {
        [Theory]
        [InlineData(null, Icon.None)]
        [InlineData("fog", Icon.Fog)]
        [InlineData("hail", Icon.None)]
        [InlineData("partly-cloudy-night", Icon.PartlyCloudyNight)]
        public void IconDeserializationUnitTest(string name, object icon)
        {
            var jsonString = $"{{\"icon\":\"{name}\"}}";
            var deserializedObject = JsonConvert.DeserializeObject<SampleIconObject>(jsonString);
            Assert.Equal((Icon)icon, deserializedObject.Icon);
        }

        [Theory]
        [InlineData(null, PrecipitationType.None)]
        [InlineData("rain", PrecipitationType.Rain)]
        [InlineData("Rain", PrecipitationType.Rain)]
        [InlineData("cows", PrecipitationType.None)]
        public void PrecipitationTypeDeserializationUnitTest(string name, object precipType)
        {
            var jsonString = $"{{\"preciptype\":\"{name}\"}}";
            var deserializedObject = JsonConvert.DeserializeObject<SamplePrecipitationTypeObject>(jsonString);
            Assert.Equal((PrecipitationType)precipType, deserializedObject.PrecipType);
        }

        private class SampleIconObject
        {
            public Icon Icon { get; set; }
        }

        private class SamplePrecipitationTypeObject
        {
            public PrecipitationType PrecipType { get; set; }
        }
    }
}