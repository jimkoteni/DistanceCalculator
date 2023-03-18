using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DistanceCalculator.Business.DistanceCalculator;
using DistanceCalculator.Business.Integrations.AirportService;
using FluentValidation;
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
		/// Query validator.
		/// </summary>
		public class Validator : AbstractValidator<Query>
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="Validator"/> class.
			/// </summary>
			public Validator()
			{
				RuleFor(q => q.FromAirportCode).SetValidator(new AirportCodeValidator());
				RuleFor(q => q.ToAirportCode).SetValidator(new AirportCodeValidator());
			}
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
			private readonly IDistanceCalculator _calculator;
			private readonly ILogger<Query> _logger;
			private readonly IMapper _mapper;

			/// <summary>
			/// Initializes a new instance of the <see cref="Handler"/> class.
			/// </summary>
			/// <param name="airportService">Airport service.</param>
			/// <param name="calculator">Distance calculator.</param>
			/// <param name="logger">Logger.</param>
			/// <param name="mapper">Mapper.</param>
			public Handler(
				IAirportService airportService,
				IDistanceCalculator calculator,
				ILogger<Query> logger,
				IMapper mapper)
			{
				_airportService = airportService;
				_calculator = calculator;
				_logger = logger;
				_mapper = mapper;
			}

			/// <inheritdoc/>
			public async Task<Result> Handle(Query query, CancellationToken ct)
			{
				_logger.LogDebug("Retrieving airports: {QueryFromAirportCode} and {QueryToAirportCode}",
					query.FromAirportCode, query.ToAirportCode);

				var fromAirport = await _airportService.GetAirport(query.FromAirportCode, ct);
				var toAirport = await _airportService.GetAirport(query.ToAirportCode, ct);

				var distance = _calculator.GetDistance(
					_mapper.Map<DistanceCalculator.Models.Point>(fromAirport.Location),
					_mapper.Map<DistanceCalculator.Models.Point>(toAirport.Location));

				return new Result { Distance = distance };
			}
		}
	}
}