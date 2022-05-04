using GBM.Challenge.Transactions.Application.Interfaces.RegisterMovement;
using GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Infrastructure.RegisterMovement
{
    public class RegisterOperation : IRegisterOperation
    {
        public void OnMovementApplied(int accountID,OrderModelCmd OrderModel)
        {
            throw new NotImplementedException();
        }
    }
}
