using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.S._2018.Popivnenko._14.BankAccountProj.Exception;
using NET.S._2018.Popivnenko._14.BankAccountProj.Implementation;
using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NET.S._2018.Popivnenko._14.Tests
{
    [TestFixture]
    public class BankAccountTester
    {
        [Test]
        public void BasicAccountTest()
        {
            BankAccount bankAccount = new BankAccount("41513531", "name", "surname", CardType.Gold);
            Assert.IsNotNull(bankAccount.GetHolderName());
        }

        [Test]
        public void BasicAccountTest1()
        {
            BankAccount bankAccount = new BankAccount("41513531", "name", "surname", CardType.Gold);
            Assert.IsNotNull(bankAccount.GetSerialNumber());
        }

        [Test]
        public void BasicAccountTest2()
        {
            BankAccount bankAccount = new BankAccount("41513531", "name", "surname", CardType.Gold);
            Func<int, int> f = delegate(int i) { return i + 1; };
            bankAccount.ChangePoints(f);
            Assert.IsTrue(bankAccount.BonusPoints > 0);
        }

        [Test]
        public void TestWithdraw()
        {
            BankAccount bankAccount = new BankAccount("41513531", "name", "surname", CardType.Gold);
            bankAccount.AddFunds(5);
            Assert.Throws<InsufficientFundsException>(() => bankAccount.WithdrawFunds(10));
        }
    }
}
