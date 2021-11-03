using System;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.API.Caching;
using CQRS.MediatR.API.DTOs;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CQRS.MediatR.API.CQRS.PipelineBehaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheable
        where TResponse : CqrsResponse
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

        public CachingBehavior(IMemoryCache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Type requestName = request.GetType();
            _logger.LogInformation($"{requestName} is configured for caching.");

            // Check to see if the item is inside the cache
            if (_cache.TryGetValue(request.CacheKey, out TResponse response))
            {
                _logger.LogInformation($"Returning cached value for {requestName}.");
                return response;
            }

            // Item is not in the cache, execute request and add to cache
            _logger.LogInformation($"{requestName} Cache Key: {request.CacheKey} is not inside the cache, executing request.");
            
            response = await next();
            _cache.Set(request.CacheKey, response);
            
            return response;
        }
    }
}
