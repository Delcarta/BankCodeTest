using BankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankGrab
{
    public class RobTheBank
    {
        public List<Bank> GetBanks()
        {
            using (var db = new BankContext())
            {
                var fetch = from bank in db.Banks
                            orderby bank.BankName
                            select bank;

                return fetch.ToList();
            }
        }

        public List<AccountOwner> GetAccountOwners(int BankID)
        {
            using (var db = new BankContext())
            {
                var fetch = from owners in db.AccountOwners
                            where owners.BankID == BankID
                            orderby owners.LastName
                            select owners;

                return fetch.ToList();
            }
        }

        public List<AccountDetail> GetAccountsByOwner(int OwnerID)
        {
            using (var db = new BankContext())
            {
                var fetch = from details in db.AccountDetails
                            where details.AcctOwnerID == OwnerID
                            select details;

                return fetch.ToList();
            }
        }

        public List<AccountDetail> GetAllAccounts()
        {
            using (var db = new BankContext())
            {
                var fetch = from details in db.AccountDetails
                            select details;

                return fetch.ToList();
            }
        }


        public string TransferFunds(decimal Amount, int SourceAccount, int DestinationAccount)
        {
            using (var db = new BankContext())
            {
                AccountDetail source = (from acct in db.AccountDetails
                                        where acct.AccountID == SourceAccount
                                        select acct).FirstOrDefault();

                AccountDetail dest = (from acct in db.AccountDetails
                                      where acct.AccountID == DestinationAccount
                                      select acct).FirstOrDefault();

                if (source != null && dest != null)
                {
                    if (source.AccountBalance > Amount)
                    {
                        source.AccountBalance = source.AccountBalance - Amount;
                        dest.AccountBalance = dest.AccountBalance + Amount;
                        db.SaveChanges();
                        return "Transfer Successful.";
                    }
                    else
                    {
                        return "Source account does not have enough funds.";
                    }
                }
                else
                {
                    return "Either source or destination account do not exist.";
                }
            }
        }

        public string DepositFunds(decimal Amount, int DestinationAccount)
        {
            using (var db = new BankContext())
            {
                AccountDetail source = (from acct in db.AccountDetails
                                        where acct.AccountID == DestinationAccount
                                        select acct).FirstOrDefault();

                if (source != null)
                {
                    source.AccountBalance = source.AccountBalance + Amount;
                    db.SaveChanges();
                    return "Deposit Successful.";
                }
                else
                {
                    return "Account does not exist.";
                }
            }
        }

        public string WithdrawFunds(decimal Amount, int DestinationAccount)
        {
            using (var db = new BankContext())
            {
                AccountDetail source = (from acct in db.AccountDetails
                                        where acct.AccountID == DestinationAccount
                                        select acct).FirstOrDefault();

                if (source != null)
                {
                    if (Amount > 1000 && source.AccountTypeCode == "II")
                        return "This account can not withdraw funds over $1000 at a time.";

                    if (source.AccountBalance > Amount)
                    {
                        source.AccountBalance = source.AccountBalance - Amount;
                        db.SaveChanges();
                        return "Withdrawal Successful.";
                    }
                    else
                    {
                        return "Not enough funds in account.";
                    }

                }
                else
                {
                    return "Account does not exist.";
                }
            }
        }
    }
}
