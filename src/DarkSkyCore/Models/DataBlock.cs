using Newtonsoft.Json;
using System.Collections.Generic;

namespace DarkSky.Models
{
	public class DataBlock
	{
		[JsonProperty(PropertyName = "data")]
		public List<DataPoint> Data { get; set; }

		[JsonProperty(PropertyName = "icon")]
		public string Icon { get; set; }

		[JsonProperty(PropertyName = "summary")]
		public string Summary { get; set; }
	}
}