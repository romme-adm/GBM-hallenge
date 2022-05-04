namespace GBM.Challenge.Transactions.Domain.Buy.Exception
{
    using System;

    /// <summary>
    /// Defines the <see cref="InsufficientBalanceException" />.
    /// </summary>
    public class InsufficientBalanceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsufficientBalanceException"/> class.
        /// </summary>
        /// <param name="businessError">The businessError<see cref="string"/>.</param>
        public InsufficientBalanceException(string businessError) : base(businessError)
        {
        }
    }
}
