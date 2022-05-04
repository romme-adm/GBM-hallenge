namespace GBM.Challenge.Transactions.Controllers
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ResponseBase" />.
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Gets or sets the ReqId.
        /// </summary>
        public string ReqId { get; set; }

        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        public int Code { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="ResponseModelOk{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class ResponseModel<T> : ResponseBase
    {
        /// <summary>
        /// Gets or sets the Data.
        /// </summary>
        public T Data { get; set; }
    }

}
