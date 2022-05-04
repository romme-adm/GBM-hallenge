using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.API.CreateInvesment.Application.Interfaces.Repositories
{
    public interface IInvestmentRepository
    {
        Domain.Investments.Investment Create(Domain.Investments.Investment investment);
        Domain.Investments.Investment GetBy(int AccountId, string Email);
    }
}
