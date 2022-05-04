using GBM.Challenge.Transactions.Application.Interfaces.Integrity;
using GBM.Challenge.Transactions.Application.Interfaces.Repositories;
using GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance;
using GBM.Challenge.Transactions.Domain.Buy.Exception;
using GBM.Challenge.Transactions.Domain.Investments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders.Buy
{
    public class BuyOrder : ISendOrder
    {
        private readonly IGetCurrentBalance _getCurrentBalance;
        private readonly IGenerateIntegrity _generateIntegrity;
        private readonly IRedisCacheRepository _redisCacheRepository;
        public BuyOrder(IGetCurrentBalance getCurrentBalance, IGenerateIntegrity generateIntegrity, IRedisCacheRepository redisCacheRepository)
        {
            _getCurrentBalance = getCurrentBalance;
            _generateIntegrity = generateIntegrity;
            _redisCacheRepository = redisCacheRepository;
        }
        public void Execute(int accountId, OrderModelCmd orderModelCmd)
        {
            InvestmentEventInfo info = _getCurrentBalance.GetByAccount(accountId);
            decimal totalTransaction = orderModelCmd.Share__Price * orderModelCmd.Total_Shares;

            if (info.Cash < totalTransaction)
            {
                throw new InsufficientBalanceException("InsufficientBalance");
            }
            if (info.Issuers is null)
            {
                info.Issuers = new List<Domain.Investment.Issuer>();
            }
            string opSeed = System.Guid.NewGuid().ToString();
            string transactionIntegrity = $"{orderModelCmd.Share__Price}_ {orderModelCmd.Total_Shares}_{accountId}_{opSeed}";
            for (int shareItem = 0; shareItem < orderModelCmd.Total_Shares; shareItem++)
            {
                info.Issuers.Add(new Domain.Investment.Issuer()
                {
                    Integrity = transactionIntegrity,
                    OperationSeed = opSeed,
                    IssuerName = orderModelCmd.Issuer_Name,
                    SharePrice = orderModelCmd.Share__Price
                }); ;
            }
            info.Cash = info.Cash - totalTransaction;
            string key = $"account_{info.Id}";
            _redisCacheRepository.Set(key, info);
        }
    }
}
