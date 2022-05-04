using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders
{
    public interface IOrderFactory
    {
        ISendOrder Create(OrderType orderType);
    }
}
