using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.API.DTOs;
using CQRS.MediatR.API.Validations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CQRS.MediatR.API.CQRS.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : CqrsResponse, new()
    {
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
        private readonly IValidationHandler<TRequest> _validationHandler;
        
        // Have 2 constructors in case the validator does not exist
        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger, IValidationHandler<TRequest> validationHandler)
            :this(logger)
        {
            _validationHandler = validationHandler;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Type requestName = request.GetType();
            if (_validationHandler == null)
            {
                _logger.LogInformation($"{requestName} does not have a validation handler configured.");
                return await next();
            }

            ValidationResult result = await _validationHandler.Validate(request);
            if (!result.IsSuccessful)
            {
                _logger.LogWarning($"Validation failed for {requestName}. Error: {result.Error}");
                return new TResponse { StatusCode = HttpStatusCode.BadRequest, ErrorMessage = result.Error};
            }

            _logger.LogInformation($"Validation successful for {requestName}.");
            return await next();
        }
    }
}
