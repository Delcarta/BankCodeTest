using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;
using NUnit.Framework;

namespace BankUnit.Tests
{
    [TestFixture]
    public class BankGrabTests
    {
        BankGrab.RobTheBank test;


        public BankGrabTests()
        {
            test = new BankGrab.RobTheBank();
        }


        [Test]
        public void GetFirstBankTest()
        {
            //Arrange 


            //Act
            Bank firstBank = test.GetBanks().FirstOrDefault();

            //Assert
            Assert.That(firstBank, Is.Not.Null);
        }

        [Test]
        public void AddFunds()
        {

            string result = test.DepositFunds((decimal)50021.36, 1);
            AccountDetail accounts = test.GetAccountsByOwner(1).Where(x => x.AccountID == 1).FirstOrDefault();

            Assert.That(result, Is.EqualTo("Deposit Successful."));
            Assert.That(accounts.AccountBalance, Is.EqualTo((decimal)51021.36));
        }

        [Test]
        public void TransferFunds()
        {
            string result = test.TransferFunds(500, 2, 3);
            AccountDetail accounts = test.GetAccountsByOwner(2).Where(x => x.AccountID == 3).FirstOrDefault();
            Assert.That(accounts.AccountBalance, Is.EqualTo((decimal)1500.00));
        }

        [Test]
        public void WithdrawFunds()
        {
            string result = test.WithdrawFunds(425, 6);
            AccountDetail accounts = test.GetAccountsByOwner(3).Where(x => x.AccountID == 6).FirstOrDefault();
            Assert.That(accounts.AccountBalance, Is.EqualTo((decimal)575.00));
        }

        [Test]
        public void InvalidWithdraw()
        {
            string result = test.WithdrawFunds(1001, 1);
            Assert.That(result, Is.EqualTo("This account can not withdraw funds over $1000 at a time."));
        }

        [OneTimeTearDown]
        public void TossOut()
        {
            test = null;

        }
    }
}
