using System.Globalization;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DistanceCalculator.Business.Infrastructure
{
	/// <summary>
	/// Service registration.
	/// </summary>
	public static class ServiceRegistration
	{
		/// <summary>
		/// Register of fluent validation.
		/// </summary>
		/// <param name="services">Service collection.</param>
		/// <returns>Service collection.</returns>
		public static IServiceCollection AddFluentValidation(this IServiceCollection services)
		{
			services.Scan(scan => scan
				.FromAssemblyOf<BusinessLayer>()
				.AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
				.AsImplementedInterfaces()
				.WithTransientLifetime());

			return services;
		}
	}
}