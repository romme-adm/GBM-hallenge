using GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Interfaces.RegisterMovement
{
    public interface IRegisterOperation
    {
        void OnMovementApplied(int accountID,OrderModelCmd OrderModel);
    }
}
