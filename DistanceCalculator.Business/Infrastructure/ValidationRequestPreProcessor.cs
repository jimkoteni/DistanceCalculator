using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace DistanceCalculator.Business.Infrastructure
{
	/// <summary>
	/// Validation PreProcessor.
	/// </summary>
	/// <typeparam name="TRequest"></typeparam>
	public class ValidationRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
	{
		private readonly IValidator<TRequest> _validator;
		private readonly ILogger<ValidationRequestPreProcessor<TRequest>> _log;
		
		/// <summary>
		///  Initializes a new instance of the <see cref="ValidationRequestPreProcessor{TRequest}"/> class.
		/// </summary>
		/// <param name="log">Logger.</param>
		/// <param name="validator">Requests validator.</param>
		public ValidationRequestPreProcessor(
			ILogger<ValidationRequestPreProcessor<TRequest>> log,
			IValidator<TRequest> validator = null)
		{
			_validator = validator;
			_log = log;
		}
		
		/// <inheritdoc />
		public async Task Process(TRequest request, CancellationToken token)
		{
			if (_validator == null) return;

			try
			{
				await _validator.ValidateAndThrowAsync(request, token);
			}
			catch (ValidationException e)
			{
				_log.LogWarning(e, e.Message);
				throw;
			}
		}
	}
}