namespace DarkSky.Services
{
	using System;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	/// <summary>
	/// <see cref="JsonConverter"/> for handling Dark Sky enum codes.
	/// </summary>
	public class DarkSkyEnumJsonConverter : StringEnumConverter
	{
		/// <summary>
		/// Deserialize the value from <paramref name="reader"/>.
		/// </summary>
		/// <param name="reader">Incoming JSON</param>
		/// <param name="objectType">Type to convert to</param>
		/// <param name="existingValue">Existing object being populated</param>
		/// <param name="serializer">Serializer being used</param>
		/// <returns>The populated object or the null instance of <paramref name="objectType"/> if an error occurs</returns>
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			try
			{
				return base.ReadJson(reader, objectType, existingValue, serializer);
			}
			catch (JsonSerializationException)
			{
				return Activator.CreateInstance(objectType, null);
			}
		}
	}
}
