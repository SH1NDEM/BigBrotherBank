using System;
using System.Collections.Generic;
using System.Text;
using BigBrotherBank.Domain.Enums;
using BigBrotherBank.Domain.Exceptions;

namespace BigBrotherBank.Domain.Entities
{
    public class Wallet
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Balance { get; private set; }
        public Currencies Currency { get; private set; }
        public WalletStatus Status { get; private set; }
        public  DateTime CreatedAtUtc { get; private set; }

        /// <summary>
        /// Пустой конструктор для EF core
        /// </summary>
        private Wallet()
        {
            Currency = Currencies.NONE;
        }

        /// <summary>
        /// Конструктор кошелька
        /// </summary>
        /// <param name="_userId">ID пользователя</param>
        /// <param name="_currency">Валюта</param>
        /// <exception cref="ArgumentException">ID пользователя не может быть пустым</exception>
        /// <exception cref="InvalidCurrencyException">Валюта не может быть пустой</exception>
        public Wallet(Guid userId, Currencies currency)
        {
            //Проверка id пользователя
            if (userId == Guid.Empty)
                throw new ArgumentException("User id cannot be empty", nameof(userId));
            //Проверка входного currency
            if (currency == Currencies.NONE)
                throw new InvalidCurrencyException("Currency cannot be empty.");

            //Присвоение значений
            Id = Guid.NewGuid();
            UserId = userId;
            Balance = 0;
            Currency = currency;
            Status = WalletStatus.Active;
            CreatedAtUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Пополнение счета
        /// </summary>
        /// <param name="_amount">Сумма</param>
        /// <exception cref="InvalidAmountException">Сумма должна быть положительной</exception>
        public void Deposit(decimal amount)
        {
            EnsureWalletIsAcrive();

            if (amount <= 0)
                throw new InvalidAmountException("Amount must be greater than zero.");

            Balance += amount;
        }

        /// <summary>
        /// Снятие денег со счета
        /// </summary>
        /// <param name="_amount">Сумма</param>
        /// <exception cref="InvalidAmountException">Сумма должна быть положительной</exception>
        /// <exception cref="InsufficientFundsException">Сумма должна быть больше имеющихся на счете</exception>
        public void Withdraw(decimal amount)
        {
            EnsureWalletIsAcrive();

            if (amount <= 0)
                throw new InvalidAmountException("Ammount must be gather than zero.");

            if (Balance < amount)
                throw new InsufficientFundsException("Insufficient funds.");

            Balance -= amount;
        }

        /// <summary>
        /// Проверка активности кошелька
        /// </summary>
        /// <exception cref="WalletBlockedException">Кошелек заблокирован</exception>
        public void EnsureWalletIsAcrive()
        {
            if (Status != WalletStatus.Active)
                throw new WalletBlockedException("Wallet is not active.");
        }

        /// <summary>
        /// Блокировка кошелька
        /// </summary>
        /// <exception cref="WalletBlockedException">Закрытый кошелек нельзя заблокировать</exception>
        public void Block()
        {
            if (Status == WalletStatus.Closed)
                throw new WalletBlockedException("Closed wallet cannot be blocked.");

            Status = WalletStatus.Blocked;
        }

        /// <summary>
        /// Закрытие кошелька
        /// </summary>
        /// <exception cref="InvalidOperationException">Нельзя закрыть кошелек с балансом больше нуля</exception>
        public void Close()
        {
            if (Balance > 0)
                throw new InvalidOperationException("Cannot close wallet with positive balance.");
        }
    }
}
