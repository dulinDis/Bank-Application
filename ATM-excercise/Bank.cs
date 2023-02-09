using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    public enum CurrencyOptions
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

    internal class Bank
    {
        //readonly
        Dictionary<long, Account> _accounts = new Dictionary<long, Account>();

        // account creation 
        public long CreateAccount(string name, string surname, CurrencyOptions currency)
        {
            return CreateAccount(name, surname, currency, 0);
        }

        public long CreateAccount(string name, string surname, CurrencyOptions currency, decimal initialBalance)
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


        // account activities
        public void ReportAccountDetails(long accountNumber) // should be a method on account
        {
            Account storedAccount = FindAccount(accountNumber);
            Console.WriteLine($"Account no: {storedAccount.AccountNumber}.");
            Console.WriteLine($"Customer: {storedAccount.UserName} {storedAccount.UserSurname}.");
            Console.WriteLine($"Account currency: {storedAccount.AccountCurrency}.");
            Console.WriteLine($"Initial balance deposited upon account creation: {storedAccount.Balance}  {storedAccount.AccountCurrency}.");
        }
        public bool CheckIfAccounExists(long accountNum)
        {
            return _accounts.ContainsKey(accountNum) ? true : false;
        }
        public Account FindAccount(long accountNum)
        {
            Account userAccount = _accounts[accountNum];
            return userAccount;
        }
        //user login and logout mehods
        public bool LogUserIn(long accountNum)
        {
            if (!CheckIfAccounExists(accountNum))
            {
                Console.WriteLine("The login attempt failed.  The user with this account number does not exist.");
                return false;
            }
            else
            {
                Account user = FindAccount(accountNum);
                user.IsLoggedIn = true;
                //accounts[accountNum].IsLoggedIn = true;
                Console.WriteLine($"The login attempt successful. Welcome {user.UserName} {user.UserSurname}.");
                return true;
            }

        }
        public bool LogUserOut(long accountNum)
        {

            if (!CheckIfAccounExists(accountNum))
            {
                Console.WriteLine("cant log out user, the user with this accoun number doesnt exist");
                return false;

            }
            else
            {
                _accounts[accountNum].IsLoggedIn = false;
                return true;

            }


        }
        public bool CheckIsUserLoggedIn(long accountNum)
        {

            if (_accounts.ContainsKey(accountNum))
            {
                return _accounts[accountNum].IsLoggedIn ? true : false;

            }
            else
            {
                return false;
            }

        }
        public void LoggedUserActions(long accountNum, AccountActions action)
        {
            // Console.WriteLine("accoun actions called with action: "+ action);
            Account userAccount = FindAccount(accountNum);

            switch (action)
            {
                case AccountActions.CheckBalance:
                    userAccount.RerieveBalance();
                    break;

                case AccountActions.TransactionHistory:
                    userAccount.retrieveTTransactionHistory();
                    break;
                case AccountActions.WithdrawATM:
                    userAccount.WithdrawFromATM(accountNum);
                    break;
                case AccountActions.DepositATM:
                    userAccount.DepositToATM(accountNum);
                    break;
                case AccountActions.SendMoney:
                    long recipientAccountNumber;
                    Console.WriteLine("What is the recipient you would like to send to? Provide accoun number (long)");
                    long.TryParse(Console.ReadLine(), out recipientAccountNumber);
                    Account recepientAccount = FindAccount(recipientAccountNumber);
                    userAccount.SendBetweenAccounts(accountNum, recepientAccount);


                    // userAccount.retrieveTTransactionHistory();
                    // userAccount.rerieveBalance();
                    // recepientAccount.retrieveTTransactionHistory();
                    // recepientAccount.rerieveBalance();

                    break;
                default:
                    Console.WriteLine("Error");
                    break;

            }
        }

        private void DepositOnAccount(long senderAccountNum, BankingOperationType bankingOperationType)
        {
            long recipientAccountNum;
            Console.WriteLine("Which account would you like to send money to? (long)");
            long.TryParse(Console.ReadLine(), out recipientAccountNum);

            Console.WriteLine("What operation");

            //  findAccount(accountNum).performTransaction(recipientAccount);
        }
    }
}
