using GBM.Challenge.API.CreateInvesment.Application.Interfaces.Repositories;
using GBM.Challenge.API.CreateInvesment.Application.Investment.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.API.CreateInvesment.Application.Investment.Commands
{
    public class CreateInvestmentCmd : ICreateInvestmentCmd
    {

        private readonly IInvestmentRepository _investmentRepository;
        private readonly IGetInvesmentInfoQuery _getInvestmentInfoQuery;
        public CreateInvestmentCmd(IInvestmentRepository investmentRepository, IGetInvesmentInfoQuery getInvestmentInfoQuery)
        {
            _investmentRepository = investmentRepository;
            _getInvestmentInfoQuery = getInvestmentInfoQuery;
        }
        public InvestmentInfoQueryModel Create(CreateInvestmentCmdModel createInvestmentModel)
        {
            var investment = FactoryInvestment.Create(createInvestmentModel.Email,createInvestmentModel.Cash,createInvestmentModel.CountryKey);
            investment = _investmentRepository.Create(investment);
            return _getInvestmentInfoQuery.Get(investment.fiInvesmentId);
        }
    }
}
