using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders
{
    public class OrderFactory : IOrderFactory
    {
        public delegate ISendOrder ServiceResolver(string key);
        ServiceResolver _serviceAccessor;
        public OrderFactory(ServiceResolver serviceAccessor)
        {
            _serviceAccessor = serviceAccessor;
        }
        public ISendOrder Create(OrderType orderType)
        {
           return _serviceAccessor(orderType.ToString());
        }
    }
}
