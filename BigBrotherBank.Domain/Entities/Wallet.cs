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

        private Wallet()
        {
            Currency = string.Empty;
        }

        //todo сделать функцианал по закрытию и блокировке счета

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

        public void Deposit(decimal _amount)
        {
            EnsureWalletIsAcrive();

            if (_amount <= 0)
                throw new InvalidAmountException("Ammount must be gather than zero.");

            Balance += _amount;
        }

        public void Withdraw(decimal _amount)
        {
            EnsureWalletIsAcrive();

            if (_amount <= 0)
                throw new InvalidAmountException("Ammount must be gather than zero.");

            if (Balance < _amount)
                throw new InsufficientFundsException("Insufficient funds.");

            Balance -= _amount;
        }

        public void EnsureWalletIsAcrive()
        {
            if (Status != WalletStatus.Active)
                throw new WalletBlockedException("Wallet is not active.");
        }
    }
}
