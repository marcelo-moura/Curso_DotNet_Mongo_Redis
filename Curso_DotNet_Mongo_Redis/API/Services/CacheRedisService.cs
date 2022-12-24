using API.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace API.Services
{
    public class CacheRedisService : ICacheRedisService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _options;

        public CacheRedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120),
            };
        }

        public T Get<T>(string key)
        {
            var cache = _distributedCache.Get(key);

            if (cache is null)
                return default;            

            var result = JsonSerializer.Deserialize<T>(cache);
            return result;
        }

        public void Set<T>(string key, T value)
        {
            var content = JsonSerializer.Serialize(value);
            _distributedCache.SetString(key, content, _options);
        }

        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }
    }
}
