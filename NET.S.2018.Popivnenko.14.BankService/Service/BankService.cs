using System;
using NET.S._2018.Popivnenko._14.BankAccountProj.Implementation;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Entities;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Interfaces;
using NET.S._2018.Popivnenko._14.DAL.Repositories;

namespace NET.S._2018.Popivnenko._14.BankService.Service
{
    public class BankService
    {
        private BankAccountRepository bankAccountRepository;
        private volatile string currentSerialNumber;

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

        public void AddFundsToAccount(string number, decimal value)
        {
            IBankAccount bankAccount = bankAccountRepository.GetByNumber(number);
            if (bankAccount != null)
            {
                bankAccount.AddFunds(value);
                bankAccount.ChangePoints(AddFundsPointChanger);
            }
        }

        public void WithdrawFundsFromAccount(string number, decimal value)
        {
            IBankAccount bankAccount = bankAccountRepository.GetByNumber(number);
            if (bankAccount != null)
            {
                bankAccount.WithdrawFunds(value);
                bankAccount.ChangePoints(WithdrawPointChanger);
            }
        }

        public void SetInavtive(string number)
        {
            IBankAccount bankAccount = bankAccountRepository.GetByNumber(number);
            bankAccount?.SetUnactive();
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
