using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Interfaces.Integrity
{
    public interface IGenerateIntegrity
    {
        string Create(string seed);
    }
}
