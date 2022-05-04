namespace GBM.Challenge.API.CreateInvesment.Persistence.Repositories
{
    using Dapper;
    using GBM.Challenge.API.CreateInvesment.Application.Interfaces.DataBase;
    using GBM.Challenge.API.CreateInvesment.Application.Interfaces.Repositories;
    using GBM.Challenge.API.CreateInvesment.Domain.Investments;

    /// <summary>
    /// Defines the <see cref="InvestmentRepository" />.
    /// </summary>
    public class InvestmentRepository : IInvestmentRepository
    {
        /// <summary>
        /// Defines the _databaseService.
        /// </summary>
        private readonly IDatabaseService _databaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvestmentRepository"/> class.
        /// </summary>
        /// <param name="databaseService">The databaseService<see cref="IDatabaseService"/>.</param>
        public InvestmentRepository(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="investment">The investment<see cref="Domain.Investments.Investment"/>.</param>
        /// <returns>The <see cref="Investment"/>.</returns>
        public Investment Create(Domain.Investments.Investment investment)
        {
            var query = "dbo.sp_CreateInvesment";
            var conn = _databaseService.GetConnection;
            var param = new DynamicParameters();
            param.Add("@pcOwnerEmail", investment.fcOwnerEmail);
            param.Add("@piCash", investment.fdcCash);
            param.Add("@pcCountryKey", investment.fcCountryKey);
            var result = SqlMapper.ExecuteScalar(conn, query, param, commandType: System.Data.CommandType.StoredProcedure);
            investment.fiInvesmentId = (int)result;
            return investment;
        }

        /// <summary>
        /// The GetBy.
        /// </summary>
        /// <param name="AccountId">The AccountId<see cref="int"/>.</param>
        /// <param name="Email">The Email<see cref="string"/>.</param>
        /// <returns>The <see cref="Investment"/>.</returns>
        public Investment GetBy(int AccountId, string Email)
        {
            var query = "dbo.sp_GetInvesmentBy";
            var conn = _databaseService.GetConnection;
            var param = new DynamicParameters();
            param.Add("@pcOwnerEmail", Email);
            param.Add("@piInvesmentId", AccountId);
            return SqlMapper.QueryFirst<Investment>(conn, query, param, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
