using System.Collections.Generic;

namespace DarkSky.Models
{
	public class Forecast
	{
		public double latitude { get; set; }
		public double longitude { get; set; }
		public string timezone { get; set; }
		public DataPoint currently { get; set; }
		public DataBlock minutely { get; set; }
		public DataBlock hourly { get; set; }
		public DataBlock daily { get; set; }
		public List<Alert> alerts { get; set; } //TODO: Check what this should be.
        public Flags flags { get; set; }
    }
}