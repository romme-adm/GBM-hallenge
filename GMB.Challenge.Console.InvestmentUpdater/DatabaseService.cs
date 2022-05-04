namespace GMB.Challenge.Console.InvestmentUpdater
{
    using Microsoft.Extensions.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="DatabaseService" />.
    /// </summary>
    public class DatabaseService
    {
        /// <summary>
        /// Defines the _config.
        /// </summary>
        IConfiguration _config;

        /// <summary>
        /// Defines the _connString.
        /// </summary>
        string _connString;

        /// <summary>
        /// Gets the GetConnection.
        /// </summary>
        public IDbConnection GetConnection => _GetConnection();

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseService"/> class.
        /// </summary>
        public DatabaseService()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("settings.json", optional: false);
            _config = builder.Build();
            _connString = this._config.GetConnectionString("investmentDB");
        }

        /// <summary>
        /// The _GetConnection.
        /// </summary>
        /// <returns>The <see cref="IDbConnection"/>.</returns>
        private IDbConnection _GetConnection()
        {
            var con = new SqlConnection(_connString);
            con.Open();
            return con;
        }
    }
}
