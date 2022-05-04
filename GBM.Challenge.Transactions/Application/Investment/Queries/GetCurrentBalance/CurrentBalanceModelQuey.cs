namespace GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="CurrentBalanceModel" />.
    /// </summary>
    public class CurrentBalanceModel
    {
        /// <summary>
        /// Gets or sets the Cash.
        /// </summary>
        public decimal Cash { get; set; }

        /// <summary>
        /// Gets or sets the Issuers.
        /// </summary>
        public List<IssuersModel> Issuers { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="IssuersModel" />.
    /// </summary>
    public class IssuersModel
    {
        /// <summary>
        /// Gets or sets the issuer_name.
        /// </summary>
        public string issuer_name { get; set; }

        /// <summary>
        /// Gets or sets the total_shares.
        /// </summary>
        public int total_shares { get; set; }

        /// <summary>
        /// Gets or sets the share_price.
        /// </summary>
        public int share_price { get; set; }

        public string set_id { get; set; }
    }
}
