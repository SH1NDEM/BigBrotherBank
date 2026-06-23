using BigBrotherBank.Domain.Enums;
using BigBrotherBank.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BigBrotherBank.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public Guid? FromWalletId { get; private set; }
        public Guid ToWalletId { get; private set; }
        public decimal Amount { get; private set; }
        public Currencies Currency { get; private set; }
        public TransactionType Type { get; private set; }
        public TransactionState Status { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        /// <summary>
        /// Создание операции депозита
        /// </summary>
        /// <param name="walletId">ID кошелька</param>
        /// <param name="amount">Сумма</param>
        /// <param name="currency">Валюта</param>
        /// <exception cref="ArgumentException">ID кошелька не может быть пустым</exception>
        /// <exception cref="InvalidAmountException">Сумма должна быть больше нуля</exception>
        /// <exception cref="InvalidCurrencyException">Валюта не может быть пустой</exception>
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

        /// <summary>
        /// Создание операции перевода
        /// </summary>
        /// <param name="fromWalletId">ID кошелька отправителя</param>
        /// <param name="toWalletId">ID кошелька получателя</param>
        /// <param name="amount">Сумма</param>
        /// <param name="currency">Валюта</param>
        /// <exception cref="ArgumentException">ID кошелька не может быть пустым</exception>
        /// <exception cref="SameWalletTransferException">ID кошельков не могут совпадать</exception>
        /// <exception cref="InvalidAmountException">Сумма должна быть больше нуля</exception>
        /// <exception cref="InvalidCurrencyException">Валюта не может быть пустой</exception>
        public void CreateTransfer(Guid fromWalletId, Guid toWalletId, decimal amount, Currencies currency)
        {
            if (toWalletId == Guid.Empty)
                throw new ArgumentException("Wallet address id cannot be empty.", nameof(toWalletId));

            if (fromWalletId == Guid.Empty)
                throw new ArgumentException("Wallet sender id cannot be empty.", nameof(fromWalletId));

            if (toWalletId == fromWalletId)
                throw new SameWalletTransferException("Sender and receiver wallets cannot be the same.");

            if (amount <= 0)
                throw new InvalidAmountException("Amount must be greater than zero.");

            if (currency == Currencies.NONE)
                throw new InvalidCurrencyException("Currency cannot be empty.");

            Id = Guid.NewGuid();
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;
            Amount = amount;
            Currency = currency;
            Type = TransactionType.Transfer;
            Status = TransactionState.Completed; //возможно изменю на ожидание когда будет финансовый шлюз
            CreatedAtUtc = DateTime.UtcNow;
        }
    }
}
