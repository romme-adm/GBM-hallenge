using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Interfaces.Repositories
{
    public interface IRedisCacheRepository
    {
         double RelativeExpHrs { get; set; }
         int SlidingExpirationMinutes { get; set; }
        T Get<T>(string key);
        T Set<T>(string key, T value);
    }
}
