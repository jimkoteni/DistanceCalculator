using System.Threading;
using System.Threading.Tasks;
using DistanceCalculator.Business.Features.Airport.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DistanceCalculator.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class DistanceController : ControllerBase
	{
		private readonly ILogger<DistanceController> _logger;
		private readonly IMediator _mediator;
		
		public DistanceController(
			ILogger<DistanceController> logger,
			IMediator mediator)
		{
			_logger = logger;
			_mediator = mediator;
		}
		
		/// <summary>
		/// Return distance between two airports. 
		/// </summary>
		/// <param name="query">Query.</param>
		/// <param name="ct">Cancellation token.</param>
		/// <returns>Operation result.</returns>
		[HttpGet]
		public Task<GetDistance.Result> GetDistance([FromQuery] GetDistance.Query query, CancellationToken ct)
			=> _mediator.Send(query, ct);
	}
}