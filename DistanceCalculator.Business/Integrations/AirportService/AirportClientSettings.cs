using System;

namespace DistanceCalculator.Business.Integrations.AirportService
{
	/// <summary>
	/// Airport client settings.
	/// </summary>
	public class AirportClientSettings
	{
		/// <summary>
		/// Airport client name.
		/// </summary>
		public const string ClientName = "AirportClient";
		
		/// <summary>
		/// Base url to airport service. 
		/// </summary>
		public Uri Endpoint { get; set; }
	}
}