namespace GMB.Challenge.Console.InvestmentUpdater
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="InvestmentEventInfo" />.
    /// </summary>
    public class InvestmentEventInfo
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Cash.
        /// </summary>
        public decimal Cash { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the CountryKey.
        /// </summary>
        public string CountryKey { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the Issuers.
        /// </summary>
        public List<IDictionary<string,object>> Issuers { get; set; }
    }
}
