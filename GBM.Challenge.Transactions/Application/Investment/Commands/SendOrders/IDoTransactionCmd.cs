using GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders
{
    public interface IDoTransactionCmd
    {
        CurrentBalanceModel Execute(int accountId, OrderModelCmd orderModelCmd);
    }
}
