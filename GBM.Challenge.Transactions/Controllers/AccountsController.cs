namespace GBM.Challenge.Transactions.Controllers
{
    using GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders;
    using GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance;
    using GBM.Challenge.Transactions.Domain.BusinessExceptionCommon;
    using GBM.Challenge.Transactions.Domain.Buy.Exception;
    using GBM.Challenge.Transactions.Domain.Sell;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;

    /// <summary>
    /// Defines the <see cref="AcountsController" />.
    /// </summary>
    [Route("gbm/challenge/v1/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        /// <summary>
        /// Defines the _doTransactionCmd.
        /// </summary>
        private readonly IDoTransactionCmd _doTransactionCmd;

        /// <summary>
        /// Defines the _getCurrentBalance.
        /// </summary>
        private readonly IGetCurrentBalance _getCurrentBalance;

        private ILogger<AccountsController> _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsController"/> class.
        /// </summary>
        /// <param name="doTransactionCmd">The doTransactionCmd<see cref="IDoTransactionCmd"/>.</param>
        /// <param name="getCurrentBalance">The getCurrentBalance<see cref="IGetCurrentBalance"/>.</param>
        public AccountsController(IDoTransactionCmd doTransactionCmd, IGetCurrentBalance getCurrentBalance, ILogger<AccountsController> logger)
        {
            _doTransactionCmd = doTransactionCmd;
            _getCurrentBalance = getCurrentBalance;
            _logger = logger;
        }

        /// <summary>
        /// The Post.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="OrderModel">The OrderModel<see cref="OrderModelCmd"/>.</param>
        /// <returns>The <see cref="ActionResult{object}"/>.</returns>
        [HttpPost]
        [Route("{id}/orders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ResponseModel<CurrentBalanceModel>> Post([FromRoute] int id, [FromBody] OrderModelCmd OrderModel)
        {
            string ReqId = this.HttpContext.TraceIdentifier;
            _logger.LogInformation($"INIT REQUEST {ReqId}");
            string[] Errors;
            try
            {
                var balance = _getCurrentBalance.GetByAccount(id);
                if (balance is null)
                {
                    Errors = new string[] { "AccountNotFound" };
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        Errors = Errors,
                        Code = StatusCodes.Status404NotFound,
                        ReqId = ReqId
                    });
                }
                CurrentBalanceModel model = _doTransactionCmd.Execute(id, OrderModel);
                _logger.LogInformation($"RETURNING REQUEST {ReqId}");
                return StatusCode(StatusCodes.Status201Created, new ResponseModel<CurrentBalanceModel>()
                {
                    Data = model,
                    Code = StatusCodes.Status201Created,
                    ReqId = ReqId
                });
            }
            catch (InsufficientBalanceException ex)
            {
                Errors = new string[] { ex.Message };
                _logger.LogError($"RETURNING REQUEST ERROR {ReqId} { ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Data = ErrorResponse(id),
                    Errors = Errors,
                    Code = StatusCodes.Status500InternalServerError,
                    ReqId = ReqId
                });
            }
            catch (DuplicateOperationException ex)
            {
                Errors = new string[] { ex.Message };
                _logger.LogError($"RETURNING REQUEST ERROR {ReqId} { ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Data = ErrorResponse(id),
                    Errors = Errors,
                    Code = StatusCodes.Status500InternalServerError,
                    ReqId = ReqId
                });
            }
            catch (InsufficientStockException ex)
            {
                Errors = new string[] { ex.Message };
                _logger.LogError($"RETURNING REQUEST ERROR {ReqId} { ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Data = ErrorResponse(id),
                    Errors = Errors,
                    Code = StatusCodes.Status500InternalServerError,
                    ReqId = ReqId
                });
            }
            catch (Exception ex) 
            {
                Errors = new string[] { "InternalError" };
                _logger.LogError($"RETURNING REQUEST ERROR {ReqId} { ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Errors = Errors,
                    Code = StatusCodes.Status500InternalServerError,
                    ReqId = ReqId
                });
            }
        }

        /// <summary>
        /// The ErrorResponse.
        /// </summary>
        /// <param name="accountId">The accountId<see cref="int"/>.</param>
        /// <returns>The <see cref="CurrentBalanceModel"/>.</returns>
        private CurrentBalanceModel ErrorResponse(int accountId)
        {
            var balance = _getCurrentBalance.GetByGroup(accountId);
            balance.Issuers = new System.Collections.Generic.List<IssuersModel>();
            return balance;
        }
    }
}
