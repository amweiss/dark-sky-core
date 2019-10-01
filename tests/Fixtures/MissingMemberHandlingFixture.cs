namespace DarkSkyCore.Tests.Fixtures
{
    using System.Threading.Tasks;
    using DarkSky.Services;
    using Newtonsoft.Json;

    class MissingMemberHandlingFixture : IJsonSerializerService
    {
        public async Task<T> DeserializeJsonAsync<T>(Task<string> json)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };

            return JsonConvert.DeserializeObject<T>(await json.ConfigureAwait(false), jsonSettings);
        }
    }
}