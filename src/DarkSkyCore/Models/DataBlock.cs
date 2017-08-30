namespace DarkSky.Models
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	/// <summary>
	/// A data block object represents the various weather phenomena occurring over a period of time.
	/// </summary>
	public class DataBlock
	{
		/// <summary>
		/// An List of <see cref="DataPoint"/>, ordered by time, which together describe the weather
		/// conditions at the requested location over time.
		/// </summary>
		[JsonProperty(PropertyName = "data")]
		public List<DataPoint> Data { get; set; }

		/// <summary>
		/// A machine-readable text summary of this data block.
		/// <para>(May take on the same values as the <see cref="Icon"/> property of data points.)</para>
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "icon")]
		public string Icon { get; set; }

		/// <summary>
		/// A human-readable summary of this data block.
		/// </summary>
		/// <remarks>optional</remarks>
		[JsonProperty(PropertyName = "summary")]
		public string Summary { get; set; }
	}
}