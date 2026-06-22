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
        public string Currency { get; private set; }
        public WalletStatus Status { get; private set; }
        public  DateTime CreatedAtUtc { get; private set; }

        /// <summary>
        /// Пустой конструктор для EF core
        /// </summary>
        private Wallet()
        {
            Currency = string.Empty;
        }

        /// <summary>
        /// Конструктор кошелька
        /// </summary>
        /// <param name="_userId">ID пользователя</param>
        /// <param name="_currency">Валюта</param>
        /// <exception cref="ArgumentException">ID пользователя не может быть пустым</exception>
        /// <exception cref="InvalidCurrencyException">Валюта не может быть пустой</exception>
        public Wallet(Guid _userId, string _currency)
        {
            //Проверка id пользователя
            if (_userId == Guid.Empty)
                throw new ArgumentException("User id cannot be empty", nameof(_userId));
            //Проверка входного currency
            if (string.IsNullOrWhiteSpace(_currency))
                throw new InvalidCurrencyException("Currency cannot be empty.");

            //Присвоение значений
            Id = Guid.NewGuid();
            UserId = _userId;
            Balance = 0;
            Currency = _currency.ToUpper();
            Status = WalletStatus.Active;
            CreatedAtUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Пополнение счета
        /// </summary>
        /// <param name="_amount">Сумма</param>
        /// <exception cref="InvalidAmountException">Сумма должна быть положительной</exception>
        public void Deposit(decimal _amount)
        {
            EnsureWalletIsAcrive();

            if (_amount <= 0)
                throw new InvalidAmountException("Ammount must be gather than zero.");

            Balance += _amount;
        }

        /// <summary>
        /// Снятие денег со счета
        /// </summary>
        /// <param name="_amount">Сумма</param>
        /// <exception cref="InvalidAmountException">Сумма должна быть положительной</exception>
        /// <exception cref="InsufficientFundsException">Сумма должна быть больше имеющихся на счете</exception>
        public void Withdraw(decimal _amount)
        {
            EnsureWalletIsAcrive();

            if (_amount <= 0)
                throw new InvalidAmountException("Ammount must be gather than zero.");

            if (Balance < _amount)
                throw new InsufficientFundsException("Insufficient funds.");

            Balance -= _amount;
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
