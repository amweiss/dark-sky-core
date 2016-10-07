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
		
		[JsonProperty(PropertyName="units")]
		public string Units { get; set; }
	}
}