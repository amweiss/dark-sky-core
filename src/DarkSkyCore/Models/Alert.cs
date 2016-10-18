using System;
using Newtonsoft.Json;

namespace DarkSky.Models
{
	public class Alert
	{
		[JsonProperty(PropertyName="description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "time")]
		public long Time { get; set; }

		[JsonProperty(PropertyName="expires")]
		public long Expires { get; set; }

		[JsonProperty(PropertyName="title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName="uri")]
		public Uri Uri { get; set; }
	}
}