using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp;
using BankApp.Models;

namespace BankTester
{
    class Program
    {
        static void Main(string[] args)
        {
            
            BankGrab.RobTheBank test = new BankGrab.RobTheBank();

            Bank GlobalBank = test.GetBanks().First();
            Console.WriteLine(GlobalBank.BankName);

            
            List<AccountOwner> own = new List<AccountOwner>();
            own = test.GetAccountOwners(GlobalBank.BankID);
            foreach (AccountOwner owner in own)
            {
                Console.WriteLine(String.Format("{0}, {1}, {2}", owner.FirstName, owner.LastName, owner.AcctOwnerID));
            }

         
            Console.WriteLine(test.TransferFunds(2000, 2, 3));
            Console.WriteLine(test.TransferFunds(50, 2, 1));
            Console.WriteLine(test.TransferFunds(425, 3, 1));

            Console.WriteLine(test.WithdrawFunds(425, 3));
            Console.WriteLine(test.WithdrawFunds(20, 2));
            Console.WriteLine(test.WithdrawFunds(1001, 1));

            Console.WriteLine(test.DepositFunds((decimal)50021.36, 1));
            Console.WriteLine(test.DepositFunds((decimal)365.23, 2));
            Console.WriteLine(test.DepositFunds((decimal)1071.32, 3));
            Console.WriteLine(test.DepositFunds((decimal)50021.36, 4));
            Console.WriteLine(test.DepositFunds((decimal)365.23, 5));
            Console.WriteLine(test.DepositFunds((decimal)1071.32, 6));

            List<AccountDetail> accounts = test.GetAccountsByOwner(own.First().AcctOwnerID);
            foreach (AccountDetail acct in accounts)
            {
                Console.WriteLine(acct.AccountBalance);
            }





            Console.ReadKey();
        }

       
    }
}
