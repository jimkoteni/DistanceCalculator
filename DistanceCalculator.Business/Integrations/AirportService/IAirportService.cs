using System;
using System.Threading;
using System.Threading.Tasks;
using DistanceCalculator.Business.Integrations.AirportService.Models;
using Microsoft.Extensions.Logging;

namespace DistanceCalculator.Business.Integrations.AirportService
{
	/// <summary>
	/// Service to get airport info.
	/// </summary>
	public interface IAirportService
	{
		/// <summary>
		/// Return info about airport. 
		/// </summary>
		/// <param name="airportCode">Airport code.</param>
		/// <param name="ct">Cancellation token</param>
		/// <returns>Airport info.</returns>
		Task<Airport> GetAirport(string airportCode, CancellationToken ct);
	}

	/// <inheritdoc/>
	public class AirportService : IAirportService
	{
		private readonly IAirportClientFactory _airportClientFactory;
		private readonly ILogger<IAirportService> _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="AirportService"/> class.
		/// </summary>
		/// <param name="airportClientFactory">Airport service factory.</param>
		/// <param name="logger">Logger.</param>
		public AirportService(
			IAirportClientFactory airportClientFactory,
			ILogger<IAirportService> logger)
		{
			_airportClientFactory = airportClientFactory;
			_logger = logger;
		}

		/// <inheritdoc/>
		public async Task<Airport> GetAirport(string airportCode, CancellationToken ct)
		{
			Airport airport;

			try
			{
				airport = await _airportClientFactory.Create().GetAirport(airportCode, ct);
				_logger.LogInformation("Airport pickup request completed successfully");
			}
			catch (Exception e)
			{
				_logger.LogError("Airport pickup request failed: {ExceptionMessage}", e.Message);
				throw;
			}
			

			return airport;
		}
	}
}