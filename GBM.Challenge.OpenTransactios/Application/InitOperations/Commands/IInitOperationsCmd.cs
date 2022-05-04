using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.OpenTransactios.Application.InitOperations.Commands
{
    public interface IInitOperationsCmd
    {
        void Init(OpenTransactionsModel openTransactionsModel);
    }
}
