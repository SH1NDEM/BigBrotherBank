using BigBrotherBank.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigBrotherBank.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        /// <summary>
        /// Конструктор пользователя
        /// </summary>
        /// <param name="email">Почта</param>
        /// <param name="passwordHash">Хеш пароля</param>
        /// <exception cref="ArgumentNullException">Почта не может быть пустой</exception>
        /// <exception cref="InvalidPasswordHashException">Хеш пароля не может быть пустой</exception>
        public User(string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("Email cannot be empty");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new InvalidPasswordHashException("Password cannot be empty");

            Id = Guid.NewGuid();
            PasswordHash = passwordHash;
            Email = email.Trim().ToLower();
            CreatedAtUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Смена почты пользователя
        /// </summary>
        /// <param name="email">Почта</param>
        /// <exception cref="ArgumentNullException">Почта не может быть пустой</exception>
        public void ChangeEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("Email cannot be empty");

            Email = email.Trim().ToLower();
        }

        /// <summary>
        /// Смена хеша пароля пользователя
        /// </summary>
        /// <param name="passwordHash">Хеш пароля</param>
        /// <exception cref="InvalidPasswordHashException">Значение хеша пароля не может быть пустым</exception>
        public void ChangePasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new InvalidPasswordHashException("Password cannot be empty");

            PasswordHash = passwordHash;
        }
    }
}
