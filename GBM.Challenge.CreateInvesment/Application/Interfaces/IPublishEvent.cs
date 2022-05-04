using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.API.CreateInvesment.Application.Interfaces
{
    public interface IPublishEvent
    {
        /// <summary>
        /// The Publish.
        /// </summary>
        /// <param name="@event">The event<see cref="object"/>.</param>
        void Publish(object @event);
    }
}
