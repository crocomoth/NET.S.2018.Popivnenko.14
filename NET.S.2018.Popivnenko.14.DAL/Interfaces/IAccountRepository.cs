using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Interfaces;

namespace NET.S._2018.Popivnenko._14.DAL.Interfaces
{
    public interface IAccountRepository
    {
        IBankAccount GetById(int id);

        IBankAccount GetByNAmeAndSurname(string name, string surname);

        IBankAccount GetByNumber(string number);

        void AddAccount(IBankAccount account);

        void RemoveAccount(IBankAccount account);

        void RemoveAccount(string number);
    }
}
