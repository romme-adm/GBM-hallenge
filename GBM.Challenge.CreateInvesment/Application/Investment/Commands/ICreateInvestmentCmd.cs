using GBM.Challenge.API.CreateInvesment.Application.Investment.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.API.CreateInvesment.Application.Investment.Commands
{
    public interface ICreateInvestmentCmd
    {
        InvestmentInfoQueryModel Create(CreateInvestmentCmdModel createInvestmentModel);
    }
}
