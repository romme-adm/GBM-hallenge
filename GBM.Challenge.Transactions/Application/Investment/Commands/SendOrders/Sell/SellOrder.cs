namespace GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders.Sell
{
    using GBM.Challenge.Transactions.Application.Interfaces.Integrity;
    using GBM.Challenge.Transactions.Application.Interfaces.Repositories;
    using GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance;
    using GBM.Challenge.Transactions.Domain.Investments;
    using GBM.Challenge.Transactions.Domain.Sell;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="SellOrder" />.
    /// </summary>
    public class SellOrder : ISendOrder
    {
        /// <summary>
        /// Defines the _getCurrentBalance.
        /// </summary>
        private readonly IGetCurrentBalance _getCurrentBalance;

        /// <summary>
        /// Defines the _generateIntegrity.
        /// </summary>
        private readonly IGenerateIntegrity _generateIntegrity;

        /// <summary>
        /// Defines the _redisCacheRepository.
        /// </summary>
        private readonly IRedisCacheRepository _redisCacheRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SellOrder"/> class.
        /// </summary>
        /// <param name="getCurrentBalance">The getCurrentBalance<see cref="IGetCurrentBalance"/>.</param>
        /// <param name="generateIntegrity">The generateIntegrity<see cref="IGenerateIntegrity"/>.</param>
        /// <param name="redisCacheRepository">The redisCacheRepository<see cref="IRedisCacheRepository"/>.</param>
        public SellOrder(IGetCurrentBalance getCurrentBalance, IGenerateIntegrity generateIntegrity, IRedisCacheRepository redisCacheRepository)
        {
            _getCurrentBalance = getCurrentBalance;
            _generateIntegrity = generateIntegrity;
            _redisCacheRepository = redisCacheRepository;
        }

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="accountId">The accountId<see cref="int"/>.</param>
        /// <param name="orderModelCmd">The orderModelCmd<see cref="OrderModelCmd"/>.</param>
        public void Execute(int accountId, OrderModelCmd orderModelCmd)
        {
            InvestmentEventInfo info = _getCurrentBalance.GetByAccount(accountId);
            if (info.Issuers is null || info.Issuers.Count == 0)
            {
                throw new InsufficientStockException("InsufficientStockException");
            }
            if (!info.Issuers.Select(iss => iss.OperationSeed == orderModelCmd.Set_Id).Any())
            {
                throw new InsufficientStockException("InsufficientStockException");
            }
            if (info.Issuers.Select(iss => iss.OperationSeed == orderModelCmd.Set_Id).Count() < orderModelCmd.Total_Shares)
            {
                throw new InsufficientStockException("InsufficientStockException");
            }
            decimal sell = orderModelCmd.Share__Price * orderModelCmd.Total_Shares;
            info.Cash += sell;
            var issuers = info.Issuers.Where(iss => iss.OperationSeed == orderModelCmd.Set_Id).ToList();
            issuers.RemoveRange(0, orderModelCmd.Total_Shares);
            info.Issuers.RemoveAll(iss => iss.OperationSeed == orderModelCmd.Set_Id);
            info.Issuers.AddRange(issuers);
            string key = $"account_{info.Id}";
            _redisCacheRepository.Set(key, info);
        }
    }
}
