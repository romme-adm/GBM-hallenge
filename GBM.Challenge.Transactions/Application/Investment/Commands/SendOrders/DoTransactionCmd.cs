namespace GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders
{
    using GBM.Challenge.Transactions.Application.Interfaces.Integrity;
    using GBM.Challenge.Transactions.Application.Interfaces.RegisterMovement;
    using GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance;
    using GBM.Challenge.Transactions.Domain.BusinessExceptionCommon;
    using System;

    /// <summary>
    /// Defines the <see cref="DoTransactionCmd" />.
    /// </summary>
    public class DoTransactionCmd : IDoTransactionCmd
    {
        /// <summary>
        /// Defines the _generateIntegrity.
        /// </summary>
        private readonly IGenerateIntegrity _generateIntegrity;

        /// <summary>
        /// Defines the _orderFactory.
        /// </summary>
        private readonly IOrderFactory _orderFactory;

        /// <summary>
        /// Defines the _getCurrentBalance.
        /// </summary>
        private readonly IGetCurrentBalance _getCurrentBalance;

        /// <summary>
        /// Defines the _registerOperation.
        /// </summary>
        private readonly IRegisterOperation _registerOperation;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoTransactionCmd"/> class.
        /// </summary>
        /// <param name="orderFactory">The orderFactory<see cref="IOrderFactory"/>.</param>
        /// <param name="getCurrentBalance">The getCurrentBalance<see cref="IGetCurrentBalance"/>.</param>
        /// <param name="registerOperation">The registerOperation<see cref="IRegisterOperation"/>.</param>
        /// <param name="generateIntegrity">The generateIntegrity<see cref="IGenerateIntegrity"/>.</param>
        public DoTransactionCmd(IOrderFactory orderFactory, IGetCurrentBalance getCurrentBalance, IRegisterOperation registerOperation, IGenerateIntegrity generateIntegrity)
        {
            _orderFactory = orderFactory;
            _getCurrentBalance = getCurrentBalance;
            _registerOperation = registerOperation;
            _generateIntegrity = generateIntegrity;
        }

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="accountId">The accountId<see cref="int"/>.</param>
        /// <param name="OrderModel">The OrderModel<see cref="OrderModelCmd"/>.</param>
        /// <returns>The <see cref="CurrentBalanceModelQuey"/>.</returns>
        public CurrentBalanceModel Execute(int accountId, OrderModelCmd OrderModel)
        {
            string operationKey = _generateIntegrity.Create(_registerOperation.GenerateOperationId(accountId, OrderModel));
            if (_registerOperation.GetDuplicateMovenment(operationKey)) 
            {
                throw new DuplicateOperationException("DuplicateOperationException");
            }
            ISendOrder sendOrder = _orderFactory.Create((OrderType)Enum.Parse(typeof(OrderType), OrderModel.Operation));
            sendOrder.Execute(accountId, OrderModel);
            _registerOperation.OnMovementApplied(accountId, OrderModel);
            return _getCurrentBalance.GetByGroup(accountId);
        }
    }
}
