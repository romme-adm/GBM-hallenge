using GBM.Challenge.Transactions.Domain.Investments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Investment.Commands.PersistAccountInfo
{
     interface IPersistInvestmentAccountCmd
    {
        void Save(InvestmentEventInfo info);
    }
}
