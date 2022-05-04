using GBM.Challenge.Transactions.Application.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Repositories.RedisPersister
{
    public class RedisCacheRepository : IRedisCacheRepository
    {
        private readonly IDistributedCache _cache;
        public RedisCacheRepository(IDistributedCache cache)
        {
            _cache = cache;
        }
        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);

            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        public T Set<T>(string key, T value)
        {
            var timeOut = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
                SlidingExpiration = TimeSpan.FromMinutes(60)
            };
            _cache.SetString(key, JsonConvert.SerializeObject(value), timeOut);
            return value;
        }
    }
}
