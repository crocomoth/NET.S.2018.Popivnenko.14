using System;
using NET.S._2018.Popivnenko._14.BankAccountProj.Exception;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Entities;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Interfaces;

namespace NET.S._2018.Popivnenko._14.BankAccountProj.Implementation
{
    public class BankAccount : IBankAccount
    {
        private string serialNumber;
        private string holderName;
        private string holderSurname;
        public CardType cardType;
        private decimal _funds;
        private int _bonusPoints;
        private object syncLock;

        public BankAccount(string serialNumber, string holderName, string holderSurname, CardType cardType)
        {
            this.holderName = holderName ?? throw new ArgumentNullException(nameof(holderName));
            this.holderSurname = holderSurname ?? throw new ArgumentNullException(nameof(holderSurname));
            this.cardType = cardType;
            this.serialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
            this._funds = 0;
            this.BonusPoints = 0;
            this.IsOpened = true;
            syncLock = new object();
        }

        public bool IsOpened { get; protected set; }
        public int BonusPoints { get => _bonusPoints; protected set => _bonusPoints = value; }

        public void AddFunds(decimal funds)
        {
            lock (syncLock)
            {
                _funds += funds;
            }
        }

        public void WithdrawFunds(decimal funds)
        {
            if (_funds < funds)
            {
                throw new InsufficientFundsException("insufficient funds");
            }

            lock (syncLock)
            {
                _funds -= funds;
            }
        }

        public void SetUnactive()
        {
            this.IsOpened = false;
        }

        public void ChangePoints(Func<int, int> functionDelegate)
        {
            lock (syncLock)
            {
                BonusPoints += functionDelegate(BonusPoints);
                if (BonusPoints < 0)
                {
                    BonusPoints = 0;
                }
            }
        }

        public string GetHolderName()
        {
            return this.holderName;
        }

        public string GetHolderSurname()
        {
            return this.holderSurname;
        }

        public string GetSerialNumber()
        {
            return this.serialNumber;
        }
    }
}
