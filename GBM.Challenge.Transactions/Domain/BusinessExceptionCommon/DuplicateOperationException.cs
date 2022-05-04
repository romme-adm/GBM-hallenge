namespace GBM.Challenge.Transactions.Domain.BusinessExceptionCommon
{
    using System;

    /// <summary>
    /// Defines the <see cref="DuplicateOperationException" />.
    /// </summary>
    public class DuplicateOperationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateOperationException"/> class.
        /// </summary>
        /// <param name="businessError">The businessError<see cref="string"/>.</param>
        public DuplicateOperationException(string businessError) : base(businessError)
        {
        }
    }
}
