using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Domain.Sell
{
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(string error) : base(error)
        {

        }
    }
}
