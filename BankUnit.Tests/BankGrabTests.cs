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

        [Test]
        public void AttemptToOverdraftAccount()
        {
            string result = test.WithdrawFunds(1000000, 6);
            Assert.That(result, Is.EqualTo("Not enough funds in account."));
        }

        [Test]
        public void NonExistantAccount()
        {
            string result = test.WithdrawFunds(200, 100);
            Assert.That(result, Is.EqualTo("Account does not exist."));
        }

        [Test]
        public void AllAccountsHaveOwners()
        {
            //Not possible.  Due to design of db, account owner id is a non-nullable, foreign key, integer field.
            //This is present for requirements testing only.
            List<AccountDetail> accounts = test.GetAllAccounts().Where(x => x.AcctOwnerID < 1).ToList() ;
            Assert.That(accounts, Is.Empty);
        }

        [OneTimeTearDown]
        public void TossOut()
        {
            test = null;

        }
    }
}
