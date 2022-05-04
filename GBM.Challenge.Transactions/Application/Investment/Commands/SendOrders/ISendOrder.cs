using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders
{
    public interface ISendOrder
    {
        void Execute(int accountId, OrderModelCmd orderModelCmd);
    }
}
