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
        public double RelativeExpHrs { get; set; }
        public int SlidingExpirationMinutes { get; set; }
        public RedisCacheRepository(IDistributedCache cache)
        {
            RelativeExpHrs = 24;
            SlidingExpirationMinutes = 60;
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
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(RelativeExpHrs),
                SlidingExpiration = TimeSpan.FromMinutes(SlidingExpirationMinutes)
            };
            _cache.SetString(key, JsonConvert.SerializeObject(value), timeOut);
            return value;
        }
    }
}
