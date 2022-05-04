namespace GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders
{
    using GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance;
    using System;

    /// <summary>
    /// Defines the <see cref="DoTransactionCmd" />.
    /// </summary>
    public class DoTransactionCmd : IDoTransactionCmd
    {
        /// <summary>
        /// Defines the _orderFactory.
        /// </summary>
        private readonly IOrderFactory _orderFactory;

        private readonly IGetCurrentBalance _getCurrentBalance;
        /// <summary>
        /// Initializes a new instance of the <see cref="DoTransactionCmd"/> class.
        /// </summary>
        /// <param name="orderFactory">The orderFactory<see cref="IOrderFactory"/>.</param>
        public DoTransactionCmd(IOrderFactory orderFactory, IGetCurrentBalance getCurrentBalance)
        {
            _orderFactory = orderFactory;
            _getCurrentBalance = getCurrentBalance;
        }

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="orderModelCmd">The orderModelCmd<see cref="OrderModelCmd"/>.</param>
        /// <returns>The <see cref="CurrentBalanceModelQuey"/>.</returns>
        public CurrentBalanceModel Execute(int accountId,OrderModelCmd OrderModel)
        {
            ISendOrder sendOrder = _orderFactory.Create((OrderType)Enum.Parse(typeof(OrderType), OrderModel.Operation));
            sendOrder.Execute(accountId,OrderModel);
            return _getCurrentBalance.GetByGroup( accountId );
        }
    }
}
