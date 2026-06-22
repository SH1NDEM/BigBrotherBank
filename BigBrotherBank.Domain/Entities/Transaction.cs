using BigBrotherBank.Domain.Enums;
using BigBrotherBank.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BigBrotherBank.Domain.Entities
{
    internal class Transaction
    {
        public Guid Id { get; private set; }
        public Guid? FromWalletId { get; private set; }
        public Guid ToWalletId { get; private set; }
        public decimal Amount { get; private set; }
        public Currencies Currency { get; private set; }
        public TransactionType Type { get; private set; }
        public TransactionState Status { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        public void CreateDeposit(Guid walletId, decimal amount, Currencies currency)
        {
            if (walletId == Guid.Empty)
                throw new ArgumentException("Wallet id cannot be empty", nameof(walletId));

            if (amount <= 0)
                throw new InvalidAmountException("Amount must be greater than zero.");

            if (currency == Currencies.NONE)
                throw new InvalidCurrencyException("Currency cannot be empty.");

            Id = Guid.NewGuid();
            FromWalletId = null;
            ToWalletId = walletId;
            Amount = amount;
            Currency = currency;
            Type = TransactionType.Deposit;
            Status = TransactionState.Completed; //возможно изменю на ожидание когда будет финансовый шлюз
            CreatedAtUtc = DateTime.UtcNow;
        }



        //CreateTransfer
    }
}
