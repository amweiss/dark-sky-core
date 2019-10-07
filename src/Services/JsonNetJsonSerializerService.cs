namespace DarkSky.Services
{
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    ///     Interface to use for handling JSON serialization via Json.NET
    /// </summary>
    public class JsonNetJsonSerializerService : IJsonSerializerService
    {
        JsonSerializerSettings _jsonSettings = new JsonSerializerSettings();

        /// <summary>
        ///     The method to use when deserializing a JSON object.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The resulting object from <paramref name="json" />.</returns>
        public async Task<T> DeserializeJsonAsync<T>(Task<string> json)
        {
            try
            {
                return (json != null)
                    ? JsonConvert.DeserializeObject<T>(await json.ConfigureAwait(false), _jsonSettings)
                    : default;
            }
            catch (JsonReaderException e)
            {
                throw new FormatException("Json Parsing Error", e);
            }
        }
    }
}