namespace DarkSkyCore.Tests.Fixtures
{
    using System.Globalization;
    using System.Threading.Tasks;
    using DarkSky.Services;
    using Newtonsoft.Json;

    class CultureSettingFixture : IJsonSerializerService
    {
        public async Task<T> DeserializeJsonAsync<T>(Task<string> json)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                Culture = new CultureInfo("de-DE")
            };

            return JsonConvert.DeserializeObject<T>(await json.ConfigureAwait(false), jsonSettings);
        }
    }
}