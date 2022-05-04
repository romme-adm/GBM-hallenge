using GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Interfaces.RegisterMovement
{
    public interface IRegisterOperation
    {
        string GenerateOperationId(int accountID, OrderModelCmd OrderModel);
        void OnMovementApplied(int accountID,OrderModelCmd OrderModel);

        bool GetDuplicateMovenment(string movenmentKey);
    }
}
