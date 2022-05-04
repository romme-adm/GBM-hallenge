using GBM.Challenge.Transactions.Domain.Investments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance
{
    public interface IGetCurrentBalance
    {
        CurrentBalanceModel GetByGroup(int accountId);
        InvestmentEventInfo GetByAccount(int accountId);
    }
}
