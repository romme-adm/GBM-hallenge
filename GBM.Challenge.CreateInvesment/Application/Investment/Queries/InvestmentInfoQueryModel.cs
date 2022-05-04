using GBM.Challenge.API.CreateInvesment.Domain.Investments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.API.CreateInvesment.Application.Investment.Queries
{
    public class InvestmentInfoQueryModel
    {
        public InvestmentInfoQueryModel()
        {
            Issuers = new List<Issuer>();
        }
        public int Id { get; set; }
        public decimal Cash { get; set; }
        public string Email { get; set; }
        public string CountryKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Issuer> Issuers { get; set; }
    }
}
