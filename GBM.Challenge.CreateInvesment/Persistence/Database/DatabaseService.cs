using GBM.Challenge.API.CreateInvesment.Application.Interfaces.DataBase;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.API.CreateInvesment.Persistence.Database
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration _configuration;
        public IDbConnection GetConnection => _GetConnection();
        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private IDbConnection _GetConnection()
        {
                var factory = DbProviderFactories.GetFactory("Microsoft.Data.SqlClient");
                var conn = factory.CreateConnection();
                conn.ConnectionString = this._configuration.GetConnectionString("investmentDB");
                conn.Open();
                return conn;
        }


    }
}
