namespace GBM.Challenge.API.CreateInvesment.Controllers
{
    using GBM.Challenge.API.CreateInvesment.Application.Interfaces;
    using GBM.Challenge.API.CreateInvesment.Application.Investment.Commands;
    using GBM.Challenge.API.CreateInvesment.Application.Investment.Queries;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the <see cref="CreateInvesmentController" />.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CreateInvesmentController : ControllerBase
    {
        /// <summary>
        /// Defines the _createInvestmentCmd.
        /// </summary>
        private readonly ICreateInvestmentCmd _createInvestmentCmd;
        private readonly IPublishEvent _publishEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInvesmentController"/> class.
        /// </summary>
        /// <param name="createInvestmentCmd">The createInvestmentCmd<see cref="ICreateInvestmentCmd"/>.</param>
        public CreateInvesmentController(ICreateInvestmentCmd createInvestmentCmd, IPublishEvent publishEvent)
        {
            _createInvestmentCmd = createInvestmentCmd;
            _publishEvent = publishEvent;
        }

        /// <summary>
        /// The Post.
        /// </summary>
        /// <param name="createInvestmentModel">The createInvestmentModel<see cref="CreateInvestmentCmdModel"/>.</param>
        /// <returns>The <see cref="ActionResult{InvestmentInfoQueryModel}"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InvestmentInfoQueryModel> Post(CreateInvestmentCmdModel createInvestmentModel)
        {
            //validar entradas
            InvestmentInfoQueryModel invInfo = _createInvestmentCmd.Create(createInvestmentModel);
            return Ok(invInfo);
        }

        /// <summary>
        /// The Post.
        /// </summary>
        /// <param name="createInvestmentModel">The createInvestmentModel<see cref="CreateInvestmentCmdModel"/>.</param>
        /// <returns>The <see cref="ActionResult{InvestmentInfoQueryModel}"/>.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InvestmentInfoQueryModel> Put()
        {
            _publishEvent.Publish(new{ InvesmentId = 1, OwnerEmail = "test@test.com"});
            return Ok();
        }
    }
}
