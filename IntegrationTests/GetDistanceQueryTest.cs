using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DistanceCalculator.Business;
using DistanceCalculator.Business.DistanceCalculator;
using DistanceCalculator.Business.Features.Airport.Queries;
using DistanceCalculator.Business.Integrations.AirportService;
using DistanceCalculator.Business.Integrations.AirportService.Models;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

using Calculator = DistanceCalculator.Business.DistanceCalculator.DistanceCalculator;

namespace IntegrationTests
{
	public class GetDistanceQueryTest
	{
		private IMediator _mediator;
		private ServiceCollection _services;
		
		[SetUp]
		public void Setup()
		{
			_services = new ServiceCollection();
			
			SetupAirportService();

			_services.AddMediatR(typeof(BusinessLayer));
			_services.AddAutoMapper(typeof(BusinessLayer));
			
			_services.AddSingleton<IDistanceCalculator, Calculator>();
			_services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
			
			var serviceProvider = _services.BuildServiceProvider();

			_mediator = serviceProvider.GetService<IMediator>();
		}

		private void SetupAirportService()
		{
			var airports = new Dictionary<string, Airport>
			{
				{ "JFK", new Airport { Location = new Coordinate { Longitude = -73.78817, Latitude = 40.642335 } } },
				{ "IST", new Airport { Location = new Coordinate { Longitude = 28.815278, Latitude = 40.976667 } } },
			};

			var airportServiceMoq = new Mock<IAirportService>();
			airportServiceMoq
				.Setup(services => services.GetAirport(It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((string code, CancellationToken _) => airports[code]);
			_services.AddSingleton(typeof(IAirportService), airportServiceMoq.Object);
		}

		[Test]
		public async Task MainTest()
		{
			// Arrange
			var request = new GetDistance.Query { FromAirportCode = "JFK", ToAirportCode = "IST" };

			// Act
			var result = await _mediator.Send(request);
			
			// Assert
			result.Distance.Should().BePositive();
		}
	}
}