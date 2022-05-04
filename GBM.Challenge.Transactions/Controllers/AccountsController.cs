namespace GBM.Challenge.Transactions.Controllers
{
    using GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders;
    using GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance;
    using GBM.Challenge.Transactions.Domain.Buy.Exception;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    /// <summary>
    /// Defines the <see cref="AcountsController" />.
    /// </summary>
    [Route("gbm/challenge/v1/acounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IDoTransactionCmd _doTransactionCmd;
        public AccountsController(IDoTransactionCmd doTransactionCmd)
        {
            _doTransactionCmd = doTransactionCmd;
        }
        /// <summary>
        /// The Post.
        /// </summary>
        /// <returns>The <see cref="ActionResult{object}"/>.</returns>
        [HttpPost]
        [Route("{id}/orders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ResponseModel<CurrentBalanceModel>> Post([FromRoute]int id,[FromBody]OrderModelCmd OrderModel)
        {
            try
            {
                CurrentBalanceModel model = _doTransactionCmd.Execute(id, OrderModel);
                return new ResponseModel<CurrentBalanceModel>()
                {
                    Data = model,
                    Code = StatusCodes.Status201Created,
                    ReqId = this.HttpContext.TraceIdentifier
                };
            }
            catch (InsufficientBalanceException ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { });
            }

            
        }
    }
}
