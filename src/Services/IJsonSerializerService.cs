namespace DarkSky.Services
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Interface to use for handling JSON serialization.
    /// </summary>
    public interface IJsonSerializerService
    {
        /// <summary>
        ///     The method to use when deserializing a JSON object.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <exception cref="System.FormatException">Throws this error on a Json parsing error.</exception>
        /// <returns>The resulting object from <paramref name="json" />.</returns>
        Task<T> DeserializeJsonAsync<T>(Task<string> json);
    }
}