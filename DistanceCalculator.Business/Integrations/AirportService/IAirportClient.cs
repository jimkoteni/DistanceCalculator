using System.Threading;
using System.Threading.Tasks;
using DistanceCalculator.Business.Integrations.AirportService.Models;
using RestEase;

namespace DistanceCalculator.Business.Integrations.AirportService
{
	/// <summary>
	/// Airport client.
	/// </summary>
	public interface IAirportClient
	{
		/// <summary>
		/// Return airport info.
		/// </summary>
		/// <param name="airportCode">Airport code.</param>
		/// <param name="ct">Cancellation token</param>
		/// <returns>Airport info.</returns>
		[Get("airports/{airportCode}")]
		Task<Airport> GetAirport([Path] string airportCode, CancellationToken ct);
	}
}