using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Interfaces;
using NET.S._2018.Popivnenko._14.DAL.Exception;
using NET.S._2018.Popivnenko._14.DAL.Interfaces;

namespace NET.S._2018.Popivnenko._14.DAL.Repositories
{
    public class BankAccountContext : DbContext, IAccountRepository
    {
        private DbSet<IBankAccount> bankAccounts;

        public BankAccountContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public IBankAccount GetById(int id)
        {
            if (id < 0)
            {
                throw new IndexOutOfRangeException(nameof(id));
            }

            var result = bankAccounts.Skip(id).Take(1);
            return result.First();
        }

        public IBankAccount GetByNAmeAndSurname(string name, string surname)
        {
            if (name == null || surname == null)
            {
                throw new ArgumentNullException();
            }
            var result = bankAccounts.FirstOrDefault(x =>
                x.GetHolderName().Equals(name) && x.GetHolderSurname().Equals(surname));
            if (result != null)
            {
                return result;
            }
            throw new DalException("element not found");
        }

        public IBankAccount GetByNumber(string number)
        {
            return bankAccounts.FirstOrDefault(x => x.GetSerialNumber() == number) ??
                   throw new DalException("no element found");
        }

        public void AddAccount(IBankAccount account)
        {
            if (account != null)
            {
                bankAccounts.Add(account);
            }
            else
            {
                throw new ArgumentNullException(nameof(account));
            }
        }

        public void RemoveAccount(IBankAccount account)
        {
            if (account != null)
            {
                bankAccounts.Remove(account);
            }
            else
            {
                throw new ArgumentNullException(nameof(account));
            }
        }

        public void RemoveAccount(string number)
        {
            if (number != null)
            {
                var accToBeRemoved = bankAccounts.FirstOrDefault(x => x.GetSerialNumber() == number);
                bankAccounts.Remove(accToBeRemoved ?? throw new DalException("element not found"));
            }
            else
            {
                throw new ArgumentNullException(nameof(number));
            }
            
        }

        public List<IBankAccount> GetAllAccounts()
        {
            return bankAccounts.ToList();
        }
    }
}
