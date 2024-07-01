using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace RentalManager.Infrastructure.CrossCutting.Commons.Caching
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IMemoryCache _cache;

        public CachingBehavior(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cacheKey = typeof(TRequest).Name;

            if (_cache.TryGetValue(cacheKey, out TResponse? response))
            {
                return response!;
            }

            response = await next();

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            _cache.Set(cacheKey, response, cacheEntryOptions);

            return response;
        }
    }
}
