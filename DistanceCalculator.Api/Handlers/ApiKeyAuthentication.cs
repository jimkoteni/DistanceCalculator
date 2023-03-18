namespace DistanceCalculator.Api.Handlers
{
	public class ApiKeyAuthentication
	{
		/// <summary>
		/// Api-key scheme name. 
		/// </summary>
		public const string DefaultScheme = "ApiKey";
		
		/// <summary>
		/// Api-key header name.
		/// </summary>
		public const string ApiKeyHeader = "Api-Key";
		
		/// <summary>
		/// Claim type.
		/// </summary>
		public const string ClaimType = "User.Id";
	}
}