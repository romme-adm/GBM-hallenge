namespace GBM.Challenge.API.CreateInvesment.Application.Investment.Queries
{
    using GBM.Challenge.API.CreateInvesment.Application.Interfaces.Repositories;

    /// <summary>
    /// Defines the <see cref="GetInvestmentInfoQuery" />.
    /// </summary>
    public class GetInvestmentInfoQuery : IGetInvesmentInfoQuery
    {
        /// <summary>
        /// Defines the _investmentRepository.
        /// </summary>
        private readonly IInvestmentRepository _investmentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetInvestmentInfoQuery"/> class.
        /// </summary>
        /// <param name="investmentRepository">The investmentRepository<see cref="IInvestmentRepository"/>.</param>
        public GetInvestmentInfoQuery(IInvestmentRepository investmentRepository)
        {
            _investmentRepository = investmentRepository;
        }

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="AcountId">The AcountId<see cref="int"/>.</param>
        /// <returns>The <see cref="InvestmentInfoQueryModel"/>.</returns>
        public InvestmentInfoQueryModel Get(int AcountId)
        {
            var investment = _investmentRepository.GetBy(AcountId, "");
            return new InvestmentInfoQueryModel()
            {
                Cash = investment.fdcCash,
                CountryKey = investment.fcCountryKey,
                CreatedAt = investment.fdCreatedAt,
                Email = investment.fcOwnerEmail,
                Id = investment.fiInvesmentId
            };
        }
    }
}
