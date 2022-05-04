using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.API.CreateInvesment.Application.Investment.Commands
{
    public class FactoryInvestment
    {
        public static Domain.Investments.Investment Create( string Email, decimal Cash, string CountryKey) 
        {
            return new Domain.Investments.Investment()
            {
                fdcCash = Cash,
                fcOwnerEmail = Email,
                fcCountryKey = CountryKey
            };
        }
    }
}
