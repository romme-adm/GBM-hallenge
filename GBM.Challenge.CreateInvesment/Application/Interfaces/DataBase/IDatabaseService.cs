using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.API.CreateInvesment.Application.Interfaces.DataBase
{
    public interface IDatabaseService
    {
        public IDbConnection GetConnection { get; }
    }
}
