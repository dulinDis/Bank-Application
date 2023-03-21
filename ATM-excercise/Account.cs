using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
namespace ATM_excercise
{
    public class Account
    {
        public Account(long accountNumber, string userName, string userSurname, Currency currencyOption, decimal initialBalance = 0)
        {
            UserName = userName;
            UserSurname = userSurname;
            Balance = initialBalance;
            AccountNumber = accountNumber;
            AccountCurrency = currencyOption;
            TransactionHistory = new List<Transaction>();
        }

        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public decimal Balance { get; set; }
        public long AccountNumber { get; }
        public bool IsLoggedIn { get; set; }
        public Currency AccountCurrency { get; set; }
        public List<Transaction> TransactionHistory { get; set; }

        //move to service


        public decimal UpdateBalance(decimal amount)
        {
            Balance = Balance + amount;
            return Balance;
        }

        public bool AddTransactionToTransactionHistory(Transaction transaction)
        {
          
              TransactionHistory.Add(transaction);
              return true;
        }
    }
}
