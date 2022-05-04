namespace GBM.Challenge.Transactions.Domain.Investment
{
    /// <summary>
    /// Defines the <see cref="Issuer" />.
    /// </summary>
    public class Issuer
    {
        public string OperationSeed { get; set; }
        public string Integrity { get; set; }
        /// <summary>
        /// Gets or sets the IssuerName.
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// Gets or sets the SharePrice.
        /// </summary>
        public int SharePrice { get; set; }
    }
}
