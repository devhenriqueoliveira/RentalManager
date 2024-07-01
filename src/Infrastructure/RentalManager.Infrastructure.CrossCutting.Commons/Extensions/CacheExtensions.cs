using Microsoft.Extensions.Caching.Memory;

namespace RentalManager.Infrastructure.CrossCutting.Commons.Extensions
{
    public static class CacheExtensions
    {
        public static async Task<T> GetOrCreateAsync<T>(
            this IMemoryCache cache,
            string key,
            Func<Task<T>> factory,
            MemoryCacheEntryOptions options = default!)
        {
            if (!cache.TryGetValue(key, out T? cacheEntry))
            {
                cacheEntry = await factory();
                cache.Set(key, cacheEntry, options ?? new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
            }
            return cacheEntry!;
        }
    }
}
