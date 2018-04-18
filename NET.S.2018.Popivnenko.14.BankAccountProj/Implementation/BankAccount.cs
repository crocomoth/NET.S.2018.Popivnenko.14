using System;
using NET.S._2018.Popivnenko._14.BankAccountProj.Exception;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Entities;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Interfaces;

namespace NET.S._2018.Popivnenko._14.BankAccountProj.Implementation
{
    /// <summary>
    /// Implements <see cref="IBankAccount"/> interface.
    /// Is a model level class with extra logic.
    /// </summary>
    public class BankAccount : IBankAccount
    {
        public CardType CardType;
        private string _serialNumber;
        private string _holderName;
        private string _holderSurname;        
        private decimal _funds;
        private int _bonusPoints;
        private object syncLock;

        /// <summary>
        /// Constructor of a BankAccount class.
        /// throws <exception cref="ArgumentNullException"></exception> case any of arguments is null.
        /// </summary>
        /// <param name="serialNumber">Serial number.</param>
        /// <param name="holderName">Holder's name.</param>
        /// <param name="holderSurname">Holder's surname.</param>
        /// <param name="cardType"><see cref="CardType"/> of a card.</param>
        public BankAccount(string serialNumber, string holderName, string holderSurname, CardType cardType)
        {
            _holderName = holderName ?? throw new ArgumentNullException(nameof(holderName));
            _holderSurname = holderSurname ?? throw new ArgumentNullException(nameof(holderSurname));
            CardType = cardType;
            _serialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
            _funds = 0;
            BonusPoints = 0;
            IsOpened = true;
            syncLock = new object();
        }

        public bool IsOpened { get; protected set; }

        public int BonusPoints { get => _bonusPoints; protected set => _bonusPoints = value; }

        /// <summary>
        /// Adds funds to the account.
        /// </summary>
        /// <param name="funds">Funds to be added.</param>
        public void AddFunds(decimal funds)
        {
            lock (syncLock)
            {
                _funds += funds;
            }
        }

        /// <summary>
        /// Withdraws money from an account.
        /// </summary>
        /// <param name="funds">Funds to be withdrawn.</param>
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

        /// <summary>
        /// Sets itslef to be inactive.
        /// </summary>
        public void SetUnactive()
        {
            IsOpened = false;
        }

        /// <summary>
        /// Sets itself to be active.
        /// </summary>
        public void SetActive()
        {
            IsOpened = true;
        }

        /// <summary>
        /// Changes Bonus points in way specified by <paramref name="functionDelegate"/>
        /// </summary>
        /// <param name="functionDelegate">Specifies way of calculation.</param>
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

        /// <summary>
        /// Holder's name.
        /// </summary>
        /// <returns>Holder's name.</returns>
        public string GetHolderName()
        {
            return _holderName;
        }

        /// <summary>
        /// Holder's surname.
        /// </summary>
        /// <returns>Holder's surname.</returns>
        public string GetHolderSurname()
        {
            return _holderSurname;
        }

        /// <summary>
        /// Serial number.
        /// </summary>
        /// <returns>Serial number.</returns>
        public string GetSerialNumber()
        {
            return _serialNumber;
        }
    }
}
