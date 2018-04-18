using System;
using NET.S._2018.Popivnenko._14.BankAccountProj.Implementation;
using NET.S._2018.Popivnenko._14.BankService.Interface;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Entities;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Interfaces;
using NET.S._2018.Popivnenko._14.DAL.Interfaces;
using NET.S._2018.Popivnenko._14.DAL.Repositories;

namespace NET.S._2018.Popivnenko._14.BankService.Service
{
    /// <summary>
    /// Implements <see cref="IBankAccountService"/> and provides basic Services for accounts.
    /// </summary>
    public class BankService : IBankAccountService
    {
        private IAccountRepository bankAccountRepository;
        private volatile string currentSerialNumber;

        /// <summary>
        /// Constructor with specified repository and serial number.
        /// throws <exception cref="ArgumentNullException"></exception> case any of parameters is null.
        /// </summary>
        /// <param name="bankAccountRepository">Repository to be used.</param>
        /// <param name="currentSerialNumber">Current serial number.</param>
        public BankService(IAccountRepository bankAccountRepository, string currentSerialNumber)
        {
            this.bankAccountRepository = bankAccountRepository ?? throw new ArgumentNullException(nameof(bankAccountRepository));
            this.currentSerialNumber = currentSerialNumber ?? throw new ArgumentNullException(nameof(currentSerialNumber));
        }

        /// <summary>
        /// Basic constructor for a class.
        /// </summary>
        public BankService()
        {
            this.bankAccountRepository = new BankAccountRepository();
            currentSerialNumber = "0";
        }

        /// <summary>
        /// Adds new account to repository.
        /// </summary>
        /// <param name="name">Name of account's owner.</param>
        /// <param name="surname">Surname of account's owner.</param>
        /// <param name="cardType"><see cref="CardType"/> of a card.</param>
        public void AddNewAccount(string name, string surname, CardType cardType)
        {
            BankAccount bankAccount = new BankAccount(GenerateNewNumber(), name, surname, cardType);
            bankAccountRepository.AddAccount(bankAccount);
        }

        public string GenerateNewNumber()
        {
            var value = Convert.ToInt32(currentSerialNumber);
            value++;
            currentSerialNumber = value.ToString();
            return currentSerialNumber;
        }

        /// <summary>
        /// Adds specified value to account's funds.
        /// </summary>
        /// <param name="number">Serial number of an account.</param>
        /// <param name="value">Value to be added.</param>
        public void AddFundsToAccount(string number, decimal value)
        {
            IBankAccount bankAccount = bankAccountRepository.GetByNumber(number);
            if (bankAccount != null)
            {
                bankAccount.AddFunds(value);
                bankAccount.ChangePoints(AddFundsPointChanger);
            }
        }

        /// <summary>
        /// Withdraws funds from account specified by serial number.
        /// </summary>
        /// <param name="number">Serial number of an account.</param>
        /// <param name="value">Value to be withdrawn.</param>
        public void WithdrawFundsFromAccount(string number, decimal value)
        {
            IBankAccount bankAccount = bankAccountRepository.GetByNumber(number);
            if (bankAccount != null)
            {
                bankAccount.WithdrawFunds(value);
                bankAccount.ChangePoints(WithdrawPointChanger);
            }
        }

        /// <summary>
        /// Sets account to be inactive.
        /// </summary>
        /// <param name="number">Serial number of an account.</param>
        public void SetInavtive(string number)
        {
            IBankAccount bankAccount = bankAccountRepository.GetByNumber(number);
            bankAccount?.SetUnactive();
        }

        /// <summary>
        /// Sets account to be active.
        /// </summary>
        /// <param name="number">Serial number of an account.</param>
        public void SetActive(string number)
        {
            IBankAccount bankAccount = bankAccountRepository.GetByNumber(number);
            bankAccount?.SetActive();
        }

        /// <summary>
        /// Gets <see cref="BankAccount"/> by a provided serial number.
        /// throws <exception cref="ArgumentNullException"></exception> if name is null
        /// </summary>
        /// <param name="name">Serial number of a card.</param>
        /// <returns>BankAccount if it finds one.</returns>
        public IBankAccount GetBankAccount(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            IBankAccount bankAccount = bankAccountRepository.GetByNumber(name);
            return bankAccount;
        }

        private int AddFundsPointChanger(int value)
        {
            return value / 100;
        }

        private int WithdrawPointChanger(int value)
        {
            return -value / 100;
        }
    }
}
