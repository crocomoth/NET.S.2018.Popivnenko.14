using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NET.S._2018.Popivnenko._14.Tests
{
    [TestFixture]
    public class BankServiceTestClass
    {
        [Test]
        public void BasicServiceTest1()
        {
            BankService.Service.BankService bankService = new BankService.Service.BankService();
            bankService.AddNewAccount("name", "surname", CardType.Basic);
            Assert.IsNotNull(bankService.GetBankAccount("1"));
        }

        [Test]
        public void BadicServiceTest2()
        {
            BankService.Service.BankService bankService = new BankService.Service.BankService();
            bankService.AddNewAccount("name", "surname", CardType.Basic);
            Assert.Throws<ArgumentNullException>(() => bankService.AddNewAccount(null, "smth", CardType.Basic));
        }

        [Test]
        public void BasicTestService3()
        {
            BankService.Service.BankService bankService = new BankService.Service.BankService();
            bankService.AddNewAccount("name", "surname", CardType.Basic);
            var bankAccount = bankService.GetBankAccount("1");
            bankService.AddFundsToAccount(bankAccount.GetSerialNumber(), 10);
            Assert.IsTrue(bankAccount.Funds > 0);
        }

        [Test]
        public void BasicTestService4()
        {
            BankService.Service.BankService bankService = new BankService.Service.BankService();
            bankService.AddNewAccount("name", "surname", CardType.Basic);
            var bankAccount = bankService.GetBankAccount("1");
            bankService.AddFundsToAccount(bankAccount.GetSerialNumber(), 10);
            bankService.WithdrawFundsFromAccount(bankAccount.GetSerialNumber(), 5);
            Assert.IsTrue(bankAccount.Funds < 10);
        }
    }
}
