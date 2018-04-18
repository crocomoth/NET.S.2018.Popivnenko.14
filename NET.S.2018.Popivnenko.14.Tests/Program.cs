using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Entities;
using NUnit.Framework;

namespace NET.S._2018.Popivnenko._14.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BankService.Service.BankService bankService = new BankService.Service.BankService();
            bankService.AddNewAccount("name", "surname", CardType.Basic);
            var smth = bankService.GetBankAccount("1");
        }
    }
}
