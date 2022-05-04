using GBM.Challenge.OpenTransactios.Application.InitOperations.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.OpenTransactios.Controllers
{
    [Route("/gbm/challenge/v1/accounts/prepare")]
    [ApiController]
    public class InitOperationsController : ControllerBase
    {
        private readonly IInitOperationsCmd _initOperationsCmd;
        public InitOperationsController(IInitOperationsCmd initOperationsCmd)
        {
            _initOperationsCmd = initOperationsCmd;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<object> Post(OpenTransactionsModel openTransactionsModel)
        {
            //validar entradas
            _initOperationsCmd.Init(openTransactionsModel);
            return Ok();
        }


    }
}
