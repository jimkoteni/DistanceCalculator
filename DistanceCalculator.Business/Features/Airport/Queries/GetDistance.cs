using System;
using System.Threading;
using System.Threading.Tasks;
using DistanceCalculator.Business.Integrations.AirportService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DistanceCalculator.Business.Features.Airport.Queries
{
	/// <summary>
	/// Return distance between two airports.
	/// </summary>
	public static class GetDistance
	{
		/// <summary>
		/// Query parameters.
		/// </summary>
		public class Query : IRequest<Result>
		{
			/// <summary>
			/// Start airport.
			/// </summary>
			public string FromAirportCode { get; set; }

			/// <summary>
			/// End airport.
			/// </summary>
			public string ToAirportCode { get; set; }
		}

		/// <summary>
		/// Query result.
		/// </summary>
		public class Result
		{
			/// <summary>
			/// Distance between airports.
			/// </summary>
			public double Distance { get; set; }
		}

		/// <summary>
		/// Handler for <see cref="Query"/>
		/// </summary>
		public class Handler : IRequestHandler<Query, Result>
		{
			private readonly IAirportService _airportService;
			private readonly ILogger<Query> _logger;

			/// <summary>
			/// Initializes a new instance of the <see cref="Handler"/> class.
			/// </summary>
			/// <param name="airportService">Airport service.</param>
			/// <param name="logger">Logger.</param>
			public Handler(
				IAirportService airportService,
				ILogger<Query> logger)
			{
				_airportService = airportService;
				_logger = logger;
			}

			/// <inheritdoc/>
			public async Task<Result> Handle(Query query, CancellationToken ct)
			{
				_logger.LogDebug("Retrieving airports: {QueryFromAirportCode} and {QueryToAirportCode}",
					query.FromAirportCode, query.ToAirportCode);

				return await Task.FromResult(new Result());
			}
		}
	}
}