namespace GBM.Challenge.Transactions.Infrastructure.RegisterMovement
{
    using GBM.Challenge.Transactions.Application.Interfaces;
    using GBM.Challenge.Transactions.Application.Interfaces.Integrity;
    using GBM.Challenge.Transactions.Application.Interfaces.RegisterMovement;
    using GBM.Challenge.Transactions.Application.Interfaces.Repositories;
    using GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders;
    using GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance;
    using System;

    /// <summary>
    /// Defines the <see cref="RegisterOperation" />.
    /// </summary>
    public class RegisterOperation : IRegisterOperation
    {
        /// <summary>
        /// Defines the _redisCacheRepository.
        /// </summary>
        IRedisCacheRepository _redisCacheRepository;

        /// <summary>
        /// Defines the _generateIntegrity.
        /// </summary>
        IGenerateIntegrity _generateIntegrity;

        IGetCurrentBalance _getCurrentBalance;
        IPublishEvent _publishEvent;
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterOperation"/> class.
        /// </summary>
        /// <param name="redisCacheRepository">The redisCacheRepository<see cref="IRedisCacheRepository"/>.</param>
        /// <param name="generateIntegrity">The generateIntegrity<see cref="IGenerateIntegrity"/>.</param>
        public RegisterOperation(IRedisCacheRepository redisCacheRepository, IGenerateIntegrity generateIntegrity, IPublishEvent publishEvent, IGetCurrentBalance getCurrentBalance)
        {
            _redisCacheRepository = redisCacheRepository;
            _generateIntegrity = generateIntegrity;
            _publishEvent = publishEvent;
            _getCurrentBalance = getCurrentBalance;
        }

        /// <summary>
        /// The GenerateOperationId.
        /// </summary>
        /// <param name="accountID">The accountID<see cref="int"/>.</param>
        /// <param name="OrderModel">The OrderModel<see cref="OrderModelCmd"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GenerateOperationId(int accountID, OrderModelCmd OrderModel)
        {
            return $"movement_{accountID}_{OrderModel.Operation}_{ OrderModel.Share__Price * OrderModel.Total_Shares }";
        }

        /// <summary>
        /// The GetDuplicateMovenment.
        /// </summary>
        /// <param name="movenmentKey">The movenmentKey<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool GetDuplicateMovenment(string movenmentKey)
        {
            OrderModelCmd cmd = _redisCacheRepository.Get<OrderModelCmd>(movenmentKey);
            return cmd != null;
        }

        /// <summary>
        /// The OnMovementApplied.
        /// </summary>
        /// <param name="accountID">The accountID<see cref="int"/>.</param>
        /// <param name="OrderModel">The OrderModel<see cref="OrderModelCmd"/>.</param>
        public void OnMovementApplied(int accountID, OrderModelCmd OrderModel)
        {
            _redisCacheRepository.SlidingExpirationMinutes = 5;
            _redisCacheRepository.RelativeExpHrs = 0.0833333;
            string key = _generateIntegrity.Create(GenerateOperationId(accountID, OrderModel));
            _redisCacheRepository.Set(key, OrderModel);
            var balance = _getCurrentBalance.GetByAccount(accountID);
            _publishEvent.Publish(balance);
        }
    }
}
