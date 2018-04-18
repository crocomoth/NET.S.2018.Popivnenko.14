using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.S._2018.Popivnenko._14.BankAccountProj.Implementation;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Entities;
using NET.S._2018.Popivnenko._14.DAL.Repositories;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NET.S._2018.Popivnenko._14.Tests
{
    [TestFixture]
    public class RepositoryTester
    {
        [Test]
        public void TestForAddingNull()
        {
            var bankAccountRepository = new BankAccountRepository();
            Assert.Throws<ArgumentNullException>(() => bankAccountRepository.AddAccount(null));
        }

        [Test]
        public void BasicTest1()
        {
            var bankAccountRepository = new BankAccountRepository();
            bankAccountRepository.AddAccount(new BankAccount("1", "a", "b", CardType.Basic));
            bankAccountRepository.AddAccount(new BankAccount("2", "c", "b", CardType.Basic));
            Assert.IsNotNull(bankAccountRepository.GetById(1));
        }

        [Test]
        public void BasicTest2()
        {
            var bankAccountRepository = new BankAccountRepository();
            bankAccountRepository.AddAccount(new BankAccount("1", "a", "b", CardType.Basic));
            Assert.IsNotNull(bankAccountRepository.GetAllAccounts());
        }
    }
}
