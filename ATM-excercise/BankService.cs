using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    public enum Currency
    {
        USD,
        EUR,
        PLN
    }// the multiaccount option yet not implemented

    public enum AccountActions
    {
        CheckBalance,
        TransactionHistory,
        WithdrawATM,
        DepositATM,
        SendMoney
    }
   
    public enum BankingOperationType
    {
        ATMTransaction,
        BankTransfer,
        BankDeposit
    }

    public class BankService
    {
        private Dictionary<long, Account> _accounts = new Dictionary<long, Account>();

        #region account creation

        public long CreateAccount(string name, string surname, Currency currency)
        {
            return CreateAccount(name, surname, currency, 0);
        }

        public long CreateAccount(string name, string surname, Currency currency, decimal initialBalance)
        {
            long accountNum = GenerateNextAccountNumber();
            Account newAccount = new Account(accountNum, name, surname, currency, initialBalance);
            _accounts.Add(newAccount.AccountNumber, newAccount);

            if (initialBalance > 0)
            {
                BankDeposit initialDeposit = new BankDeposit(accountNum, initialBalance, currency);
                newAccount.TransactionHistory.Add(initialDeposit);
            }

            return accountNum;
        }

        private long GenerateNextAccountNumber()
        {
            return _accounts.Count + 1;
        }

        #endregion

        #region account actions

        public bool CheckIfAccountExists(long accountNum)
        {
            return _accounts.ContainsKey(accountNum) ? true : false;
        }

        public Account FindAccount(long accountNum)
        {
            return _accounts[accountNum];
        }
        
        public bool LogUserIntoAccount(long accountNum, out Account account)
        {
            if (!CheckIfAccountExists(accountNum))
            {
                account = null;
                return false;
            }
            else
            {
                account = FindAccount(accountNum);
                account.IsLoggedIn = true;
                return true;
            }
        }

        public bool LogUserOutFromAccount(long accountNum)
        {
            if (!CheckIfAccountExists(accountNum))
            {
                return false;
            }
            else
            {
                _accounts[accountNum].IsLoggedIn = false;
                return true;
            }
        }

        //public bool CheckIsUserLoggedInAccount(long accountNum)
        //{
        //    if (_accounts.ContainsKey(accountNum))
        //    {
        //        return _accounts[accountNum].IsLoggedIn ? true : false;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public void LoggedUserActions(long accountNum, AccountActions action)
        //{
        //    Account userAccount = FindAccount(accountNum);

        //    switch (action)
        //    {
        //        case AccountActions.CheckBalance:
        //            //CheckBalance(userAccount);
        //            // here provide function from user interface
        //            //userAccount.RetrieveBalance();
        //            break;

        //        case AccountActions.TransactionHistory:
        //           //userAccount.RetrieveTransactionHistory();
        //            break;

        //        case AccountActions.WithdrawATM:
        //          //  userAccount.WithdrawFromATM(accountNum);
        //            break;

        //        case AccountActions.DepositATM:
        //            userAccount.DepositToATM(accountNum);
        //            break;

        //        case AccountActions.SendMoney:
        //          //  Console.WriteLine("What is the recipient you would like to send to? Provide account number (long)");
        //          //  long.TryParse(Console.ReadLine(), out long recipientAccountNumber);
        //          //  Account recepientAccount = FindAccount(recipientAccountNumber);
        //          //  userAccount.TransferToAccount(recepientAccount);

        //          // here provide function from user interface
        //            break;

        //        default:
        //            Console.WriteLine("Error");
        //            break;
        //    }
        //}
    
        #endregion
    }
}