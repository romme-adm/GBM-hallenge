using GBM.Challenge.Transactions.Application.Interfaces.Repositories;
using GBM.Challenge.Transactions.Domain.Investments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Investment.Commands.PersistAccountInfo
{
    public class PersistInvestmentAccountCmd : IPersistInvestmentAccountCmd
    {
        private readonly IRedisCacheRepository _redisCacheRepository;
        public PersistInvestmentAccountCmd(IRedisCacheRepository redisCacheRepository)
        {
            _redisCacheRepository = redisCacheRepository;
        }
        public void Save(InvestmentEventInfo info)
        {
            _redisCacheRepository.SlidingExpirationMinutes = 24;
            _redisCacheRepository.RelativeExpHrs = 60;
            string key = $"account_{info.Id}";
            _redisCacheRepository.Set(key,info);
        }
    }
}
