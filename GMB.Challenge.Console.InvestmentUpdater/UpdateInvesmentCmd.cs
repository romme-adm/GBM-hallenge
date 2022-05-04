using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GMB.Challenge.Console.InvestmentUpdater
{
    public class UpdateInvesmentCmd
    {
        DatabaseService _databaseService;
        IDbConnection _dbConnection;
        public UpdateInvesmentCmd()
        {
            _databaseService = new DatabaseService();
            _dbConnection = _databaseService.GetConnection;
        }
        public void Execute(InvestmentEventInfo cmd) 
        {
            string update = $"UPDATE [dbo].[Investment] SET fdcCash = {cmd.Cash} where fiInvesmentId={cmd.Id}";
            var command = _dbConnection.CreateCommand();
            command.CommandText = update;
            command.ExecuteNonQuery();
        }
    }
}
