using System;
using System.Collections.Generic;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Interfaces;
using NET.S._2018.Popivnenko._14.DAL.Interfaces;

namespace NET.S._2018.Popivnenko._14.DAL.Repositories
{
    /// <summary>
    /// Implements <see cref="IAccountRepository"/>
    /// provides basic funtionality of repository
    /// </summary>
    public class BankAccountRepository : IAccountRepository
    {
        private List<IBankAccount> _list;

        /// <summary>
        /// General constructor for an object.
        /// </summary>
        public BankAccountRepository()
        {
            _list = new List<IBankAccount>();
        }

        public List<IBankAccount> List { get => _list; protected set => _list = value; }

        /// <summary>
        /// Gets <see cref="IBankAccount"/> by its id.
        /// throws <exception cref="IndexOutOfRangeException"></exception> if id is greater then max id.
        /// </summary>
        /// <param name="id">Id if an account in list.</param>
        /// <returns>Account specified by id.</returns>
        public IBankAccount GetById(int id)
        {
            if (id > List.Count)
            {
                throw new IndexOutOfRangeException(nameof(id));
            }

            return List[id];
        }

        /// <summary>
        /// Returns <see cref="IBankAccount"/> by its name and surname of an owner.
        /// throws <exception cref="ArgumentNullException"></exception> case any of parameters is null.
        /// </summary>
        /// <param name="name">Name of an onwer.</param>
        /// <param name="surname">Surname of an owner.</param>
        /// <returns>Account if it is found null otherwise.</returns>
        public IBankAccount GetByNAmeAndSurname(string name, string surname)
        {
            string localName = name ?? throw new ArgumentNullException(nameof(name));
            string localSurname = surname ?? throw new ArgumentNullException(nameof(surname));
            foreach (var account in List)
            {
                if (localName.Equals(account.GetHolderName()) && localSurname.Equals(account.GetHolderSurname()))
                {
                    return account;
                }
            }

            return null;
        }

        /// <summary>
        /// Allows to find a <see cref="IBankAccount"/> by its serial number.
        /// throws <exception cref="ArgumentNullException"></exception> if number is null.
        /// </summary>
        /// <param name="number">Serial number.</param>
        /// <returns>Account if it was found, null otherwise.</returns>
        public IBankAccount GetByNumber(string number)
        {
            if (number == null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            foreach (var elem in List)
            {
                if (elem.GetSerialNumber().Equals(number))
                {
                    return elem;
                }
            }

            return null;
        }

        /// <summary>
        /// Adds account to a repository.
        /// throws <exception cref="ArgumentNullException"></exception> if account is null.
        /// </summary>
        /// <param name="account">Account to be added.</param>
        public void AddAccount(IBankAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            List.Add(account);
        }

        /// <summary>
        /// Deletes account from repository.
        /// throws <see cref="ArgumentNullException"/> if account is null.
        /// </summary>
        /// <param name="account">Account to be deleted.</param>
        public void RemoveAccount(IBankAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            List.Remove(account);
        }

        /// <summary>
        /// Gets list of <see cref="IBankAccount"/> of accounts.
        /// </summary>
        /// <returns>List of accounts.</returns>
        public List<IBankAccount> GetAllAccounts()
        {
            return _list;
        }

        /// <summary>
        /// Deletes account from repository.
        /// throws <exception cref="ArgumentNullException"></exception> if number is null.
        /// </summary>
        /// <param name="number">Serial number of an account.</param>
        public void RemoveAccount(string number)
        {
            if (number == null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            IBankAccount bankAccount = null;
            foreach (var account in List)
            {
                if (account.GetSerialNumber().Equals(number))
                {
                    bankAccount = account;
                    break;
                }
            }

            if (bankAccount != null)
            {
                List.Remove(bankAccount);
            }
        }
    }
}
