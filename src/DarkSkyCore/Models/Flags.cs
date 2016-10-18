using System.Collections.Generic;
using Newtonsoft.Json;

namespace DarkSky.Models
{
	public class Flags
	{
		[JsonProperty(PropertyName="darksky-unavailable")]
		public string DarkskyUnavailable { get; set; }

		[JsonProperty(PropertyName="metno-license")]
		public string MetnoLicense { get; set; }
		
		[JsonProperty(PropertyName="sources")]
		public List<string> Sources { get; set; }

		[JsonProperty(PropertyName = "darksky-stations")]
		public List<string> DarkskyStations { get; set; }

		[JsonProperty(PropertyName = "lamp-stations")]
		public List<string> LampStations { get; set; }

		[JsonProperty(PropertyName = "isd-stations")]
		public List<string> IsdStations { get; set; }

		[JsonProperty(PropertyName = "madis-stations")]
		public List<string> MadisStations { get; set; }

		[JsonProperty(PropertyName="units")]
		public string Units { get; set; }
	}
}