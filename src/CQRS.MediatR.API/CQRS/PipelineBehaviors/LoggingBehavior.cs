using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CQRS.MediatR.API.CQRS.PipelineBehaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // pre execution logic
            _logger.LogInformation($"{request.GetType().Name} is starting");
            Stopwatch timer = Stopwatch.StartNew();

            // execute next step in pipeline
            TResponse response = await next();

            // post execution logic
            timer.Stop();
            _logger.LogInformation($"{request.GetType().Name} has finished in {timer.ElapsedMilliseconds}ms");

            return response;
        }
    }
}
