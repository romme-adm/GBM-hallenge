using GBM.Challenge.OpenTransactios.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.OpenTransactios.Application.InitOperations.Commands
{
    public class InitOperationsCmd : IInitOperationsCmd
    {
        private readonly IPublishEvent _publishEvent;

        public InitOperationsCmd(IPublishEvent publishEvent)
        {
            _publishEvent = publishEvent;
        }
        public void Init(OpenTransactionsModel openTransactionsModel)
        {
            _publishEvent.Publish( openTransactionsModel );
        }
    }
}
