using Newtonsoft.Json;

namespace DistanceCalculator.Business.Integrations.AirportService.Models
{
	/// <summary>
	/// Airport.
	/// <remarks>Only required properties are implemented.</remarks>
	/// </summary>
	public class Airport
	{
		/// <summary>
		/// Location of airport.
		/// </summary>
		[JsonProperty(PropertyName = "location")]
		public Coordinate Location { get; set; }
	}
}