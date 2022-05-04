namespace GBM.Challenge.API.CreateInvesment.Domain.Investments
{
    using System;

    /// <summary>
    /// Defines the <see cref="Investment" />.
    /// </summary>
    public class Investment
    {
        /// <summary>
        /// Gets or sets the fiInvesmentId.
        /// </summary>
        public int fiInvesmentId { get; set; }

        /// <summary>
        /// Gets or sets the fcOwnerEmail.
        /// </summary>
        public string fcOwnerEmail { get; set; }

        /// <summary>
        /// Gets or sets the fdcCash.
        /// </summary>
        public decimal fdcCash { get; set; }

        /// <summary>
        /// Gets or sets the fcCountryKey.
        /// </summary>
        public string fcCountryKey { get; set; }

        /// <summary>
        /// Gets or sets the fdCreatedAt.
        /// </summary>
        public DateTime fdCreatedAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether fbStatus.
        /// </summary>
        public bool fbStatus { get; set; }
    }
}
