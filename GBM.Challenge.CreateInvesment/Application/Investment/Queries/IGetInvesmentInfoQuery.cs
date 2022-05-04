namespace GBM.Challenge.API.CreateInvesment.Application.Investment.Queries
{
    /// <summary>
    /// Defines the <see cref="IGetInvesmentInfoQuery" />.
    /// </summary>
    public interface IGetInvesmentInfoQuery
    {
        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="AcountId">The AcountId<see cref="int"/>.</param>
        /// <returns>The <see cref="InvestmentInfoQueryModel"/>.</returns>
        InvestmentInfoQueryModel Get(int AcountId);
    }
}
