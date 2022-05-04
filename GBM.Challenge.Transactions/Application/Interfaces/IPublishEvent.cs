using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Interfaces
{
    public interface IPublishEvent
    {
        void Publish(object @event);
    }
}
