using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace BankApp.Models
{
    public class BankDBInitializer : DropCreateDatabaseAlways<BankContext>
    {
        protected override void Seed(BankContext context)
        {
            IList<Bank> defaultBank = new List<Bank>();
            defaultBank.Add(new Bank() { BankID = 1, BankName = "United Cake Lovers" });

            IList<AccountOwner> defaultOwner = new List<AccountOwner> ();
            defaultOwner.Add(new AccountOwner() { AcctOwnerID = 1, FirstName = "Harold", LastName = "Cross", BankID = 1});
            defaultOwner.Add(new AccountOwner() { AcctOwnerID = 2, FirstName = "Mary", LastName = "Lou", BankID = 1});
            defaultOwner.Add(new AccountOwner() { AcctOwnerID = 3, FirstName = "Micheal", LastName = "Bay", BankID = 1});

            IList<AccountDetail> defaultAccount = new List<AccountDetail>();
            defaultAccount.Add(new AccountDetail() { AccountID = 1, AccountTypeCode = "II", AccountTypeFull = "Individual Investment", AccountBalance = 1000, AcctOwnerID = 1 });
            defaultAccount.Add(new AccountDetail() { AccountID = 2, AccountTypeCode = "CI", AccountTypeFull = "Corporate Investment", AccountBalance = 1000, AcctOwnerID = 1 });
            defaultAccount.Add(new AccountDetail() { AccountID = 3, AccountTypeCode = "CI", AccountTypeFull = "Corporate Investment", AccountBalance = 1000, AcctOwnerID = 2 });
            defaultAccount.Add(new AccountDetail() { AccountID = 4, AccountTypeCode = "C", AccountTypeFull = "Checking", AccountBalance = 1000, AcctOwnerID = 3 });
            defaultAccount.Add(new AccountDetail() { AccountID = 5, AccountTypeCode = "C", AccountTypeFull = "Checking", AccountBalance = 1000, AcctOwnerID = 3 });
            defaultAccount.Add(new AccountDetail() { AccountID = 6, AccountTypeCode = "C", AccountTypeFull = "Checking", AccountBalance = 1000, AcctOwnerID = 2 });

            context.Banks.AddRange(defaultBank);
            context.AccountOwners.AddRange(defaultOwner);
            context.AccountDetails.AddRange(defaultAccount);

            base.Seed(context);
        }
    }

    public class BankContext : DbContext
    {
        public BankContext() : base()
        {
            Database.SetInitializer(new BankDBInitializer());
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<AccountOwner> AccountOwners { get; set; }
        public DbSet<AccountDetail> AccountDetails { get; set; }
    }
    public class Bank 
    {
        [Key]
        public int BankID { get; set; }
        public string BankName { get; set; }

        public virtual List<AccountOwner> AccountOwners { get; set; }
    }

    public class AccountOwner
    {
        [Key]
        public int AcctOwnerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public int BankID { get; set; }


        public virtual List<AccountDetail> AccountDetails { get; set; }
    }
    public class AccountDetail
    {
        [Key]
        public int AccountID { get; set; }
        public string AccountTypeCode { get; set; }
        public string AccountTypeFull { get; set; }
        public decimal AccountBalance { get; set; }
        
        public int AcctOwnerID { get; set; }
        public virtual AccountOwner AccountOwner { get; set; }


    }
}
