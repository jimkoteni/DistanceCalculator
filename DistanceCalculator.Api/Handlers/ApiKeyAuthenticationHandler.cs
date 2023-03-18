using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;

namespace DistanceCalculator.Api.Handlers
{
	public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		/// <summary>
		/// Api-key.
		/// Must be kept in a secret vault.
		/// Used a constant only for simple implementation.
		/// </summary>
		private const string SecretApiKey = "secret_api_key";

		public ApiKeyAuthenticationHandler(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock)
			: base(options, logger, encoder, clock)
		{
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			Request.Headers.TryGetValue(ApiKeyAuthentication.ApiKeyHeader, out var apiKey);
			
			if (string.IsNullOrWhiteSpace(apiKey) || apiKey != SecretApiKey)
			{
				return AuthenticateResult.NoResult();
			}
			
			var claims = await Task.FromResult(new List<Claim>
			{
				new(ClaimsIdentity.DefaultRoleClaimType, "anyRole1"), 
				new(ClaimConstants.Scp, "access_as_user")
			});

			var identity = new ClaimsIdentity(
				claims,
				ApiKeyAuthentication.DefaultScheme,
				ApiKeyAuthentication.ClaimType,
				ClaimsIdentity.DefaultRoleClaimType);

			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, ApiKeyAuthentication.DefaultScheme);

			return AuthenticateResult.Success(ticket);
		}
	}
}