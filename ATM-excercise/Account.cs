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

        public BankTransfer TransferToAccount(Account recipientAccount, decimal amount)
        {
            BankTransfer transactionOutgoing = new BankTransfer(AccountNumber, recipientAccount.AccountNumber, BankTransferType.Outgoing, amount * (-1), AccountCurrency);
            Balance -= amount;
            TransactionHistory.Add(transactionOutgoing);

            BankTransfer transacionIncoming = new BankTransfer(AccountNumber, recipientAccount.AccountNumber, BankTransferType.Incoming, amount, AccountCurrency);
            recipientAccount.Balance += amount;
            recipientAccount.TransactionHistory.Add(transacionIncoming);
            return transactionOutgoing;
        }

        public ATMTransaction DepositToATM(decimal amount)
        {
            ATMTransaction transaction = new ATMTransaction(amount, AccountNumber, AccountCurrency);
            Balance += amount;
            TransactionHistory.Add(transaction);
            return transaction;
        }

        public ATMTransaction WithdrawFromATM(long accountNumber, decimal amount)
        {
            ATMTransaction transaction = new ATMTransaction(amount * (-1), accountNumber, AccountCurrency);
            Balance -= amount;
            TransactionHistory.Add(transaction);
            return transaction;
        }
    }
}
