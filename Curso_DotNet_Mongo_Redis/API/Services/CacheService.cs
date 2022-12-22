using API.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace API.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            var cache = _memoryCache.Get<T>(key);
            return cache;
        }

        public void Set<T>(string key, T value)
        {
            _memoryCache.Set(key, value);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }        
    }
}
