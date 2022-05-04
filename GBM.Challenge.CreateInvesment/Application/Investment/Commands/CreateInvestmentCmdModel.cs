using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.API.CreateInvesment.Application.Investment.Commands
{
    public class CreateInvestmentCmdModel
    {

        public decimal Cash { get; set; }
        public string Email { get; set; }
        public string CountryKey { get; set; }
    }
}
