using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.API.CreateInvesment.Domain.Events.OpenOperations
{
    public class OpenOperationEventInfo
    {
        /// <summary>
        /// Gets or sets the InvesmentId.
        /// </summary>
        public int InvesmentId { get; set; }

        /// <summary>
        /// Gets or sets the OwnerEmail.
        /// </summary>
        public string OwnerEmail { get; set; }
    }
}
