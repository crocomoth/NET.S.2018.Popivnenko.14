using System;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Interfaces;
using System.Collections.Generic;

namespace NET.S._2018.Popivnenko._14.DAL.Repositories
{
    public class BankAccountRepository
    {
        private List<IBankAccount> _list;

        public List<IBankAccount> List { get => _list; protected set => _list = value; }

        public IBankAccount GetById(int id)
        {
            if (id > List.Count)
            {
                throw new IndexOutOfRangeException(nameof(id));
            }

            return List[id];
        }

        public IBankAccount GetByNAmeAndSurname(string name, string surname)
        {
            string localName = name ?? throw new ArgumentNullException(nameof(name));
            string localSurname = surname ?? throw new ArgumentNullException(nameof(surname));
            foreach (var account in List)
            {
                if ((localName.Equals(account.GetHolderName())) && (localSurname.Equals(account.GetHolderSurname())))
                {
                    return account;
                }
            }

            return null;
        }

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

        public void AddAccount(IBankAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            List.Add(account);
        }

        public void RemoveAccount(IBankAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            List.Remove(account);
        }

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
