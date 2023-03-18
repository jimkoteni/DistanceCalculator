using Newtonsoft.Json;

namespace DistanceCalculator.Business.Integrations.AirportService.Models
{
	/// <summary>
	/// Coordinate.
	/// </summary>
	public class Coordinate
	{
		/// <summary>
		/// Latitude.
		/// </summary>
		[JsonProperty(PropertyName = "lat")]
		public double Latitude { get; set; }
		
		/// <summary>
		/// Longitude.
		/// </summary>
		[JsonProperty(PropertyName = "lon")]
		public double Longitude { get; set; }
	}
}