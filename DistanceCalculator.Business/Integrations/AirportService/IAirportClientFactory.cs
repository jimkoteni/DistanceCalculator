using System.Net.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestEase;

namespace DistanceCalculator.Business.Integrations.AirportService
{
	/// <summary>
	/// Factory that build an airport client.
	/// </summary>
	public interface IAirportClientFactory
	{
		/// <summary>
		/// Return of the built airport client.
		/// </summary>
		/// <returns>Airport client.</returns>
		public IAirportClient Create();
	}
	
	/// <inheritdoc/>
	public class AirportClientFactory : IAirportClientFactory
	{
		private readonly IHttpClientFactory _clientFactory;
		private readonly AirportClientSettings _airportClientSettings;
		
		private static readonly JsonSerializerSettings SerializerSettings = new()
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
		};

		/// <summary>
		/// Initializes a new instance of the <see cref="AirportClientFactory"/> class.
		/// </summary>
		/// <param name="clientFactory">Http client factory.</param>
		/// <param name="airportClientSettings">Airport client settings.</param>
		public AirportClientFactory(
			IHttpClientFactory clientFactory,
			IOptions<AirportClientSettings> airportClientSettings)
		{
			_clientFactory = clientFactory;
			_airportClientSettings = airportClientSettings.Value;
		}

		/// <inheritdoc/>
		public IAirportClient Create()
		{
			var client = _clientFactory.CreateClient(AirportClientSettings.ClientName);
			client.BaseAddress = _airportClientSettings.Endpoint;

			return new RestClient(client)
			{
				JsonSerializerSettings = SerializerSettings,
			}.For<IAirportClient>();

		}
	}
}