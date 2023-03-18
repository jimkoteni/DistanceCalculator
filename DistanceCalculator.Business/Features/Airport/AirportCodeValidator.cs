using System.Text.RegularExpressions;
using FluentValidation;

namespace DistanceCalculator.Business.Features.Airport
{
	public class AirportCodeValidator : AbstractValidator<string>
	{
		/// <summary>
		/// Regex template for airport code.
		/// </summary>
		private static readonly Regex AirportCodeRegex = new (@"^[A-Z]+$");

		private const string AirportCodeErrorMassage = "The airport code must consist of uppercase Latin characters only";
		
		public AirportCodeValidator()
		{
			RuleFor(q => q).Length(3);
			RuleFor(m => m)
				.Matches(AirportCodeRegex)
				.WithMessage(AirportCodeErrorMassage);
		}
	}
}