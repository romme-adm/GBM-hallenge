namespace GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance
{
    using GBM.Challenge.Transactions.Application.Interfaces.Repositories;
    using GBM.Challenge.Transactions.Domain.Investments;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="GetCurrentBalance" />.
    /// </summary>
    public class GetCurrentBalance : IGetCurrentBalance
    {
        /// <summary>
        /// Defines the _redisCacheRepository.
        /// </summary>
        private readonly IRedisCacheRepository _redisCacheRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCurrentBalance"/> class.
        /// </summary>
        /// <param name="redisCacheRepository">The redisCacheRepository<see cref="IRedisCacheRepository"/>.</param>
        public GetCurrentBalance(IRedisCacheRepository redisCacheRepository)
        {
            _redisCacheRepository = redisCacheRepository;
        }

        /// <summary>
        /// The GetBy.
        /// </summary>
        /// <param name="accountId">The accountId<see cref="int"/>.</param>
        /// <returns>The <see cref="CurrentBalanceModelQuey"/>.</returns>
        public CurrentBalanceModel GetByGroup(int accountId)
        {
            string key = $"account_{accountId}";
            InvestmentEventInfo infoSource = _redisCacheRepository.Get<InvestmentEventInfo>(key);
            var group = from iss in infoSource.Issuers group iss by iss.Integrity;
            return new CurrentBalanceModel()
            {
                Cash = infoSource.Cash,
                Issuers = group.Select(ig => new IssuersModel()
                {
                    issuer_name = ig.First().IssuerName,
                    share_price = ig.First().SharePrice,
                    total_shares = ig.Count()
                }).ToList(),
            };
        }

        /// <summary>
        /// The GetByAccount.
        /// </summary>
        /// <param name="accountId">The accountId<see cref="int"/>.</param>
        /// <returns>The <see cref="InvestmentEventInfo"/>.</returns>
        public InvestmentEventInfo GetByAccount(int accountId)
        {
            string key = $"account_{accountId}";
            return _redisCacheRepository.Get<InvestmentEventInfo>(key);
        }
    }
}
