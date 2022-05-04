namespace GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders
{
    /// <summary>
    /// Defines the <see cref="OrderModelCmd" />.
    /// </summary>
    public class OrderModelCmd
    {
        /// <summary>
        /// Gets or sets the Timestamp.
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the Operation.
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// Gets or sets the IssuerName.
        /// </summary>
        public string Issuer_Name { get; set; }

        /// <summary>
        /// Gets or sets the Total_Shares.
        /// </summary>
        public int Total_Shares { get; set; }

        /// <summary>
        /// Gets or sets the Share__Price.
        /// </summary>
        public int Share__Price { get; set; }
    }
}
