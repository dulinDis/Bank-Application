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
    public enum currencyOptions { USD, EUR, PLN };// make these accessible everywhere and ake subaccoutn dependng on currency
    public enum accountActions { CheckBalance, TransactionHistory, WithdrawATM, DepositATM, SendMoney };
    public enum bankingOperationTypes { ATMTransaction, BankTransfer, BankDeposit };

     internal class Bank
    {
        //readonly
        Dictionary<long, Account> accounts = new Dictionary<long, Account>();

        // account creation 
        public long createAccount(string name, string surname, currencyOptions currency)
        {
            return createAccount(name, surname, currency, 0);
        }

        public long createAccount(string name, string surname, currencyOptions currency, decimal initialBalance)
        {
            long accountNum = getNextAccountNumber();
            Account newAccount = new Account(accountNum, name, surname, currency, initialBalance);
            accounts.Add(newAccount.AccountNumber, newAccount);
            if (initialBalance > 0)
            {
                BankDeposit initialDeposit = new BankDeposit(accountNum, initialBalance, currency);
                newAccount.TransactionHistory.Add(initialDeposit);
            }
            return accountNum;
        }

        private long getNextAccountNumber()
        {
            return accounts.Count + 1;
        }


        // account activities
        public void reportAccountDetails(long accountNumber) // should be a method on account
        {
            Account storedAccount = (Account)accounts[accountNumber];
            Console.WriteLine($"Account no: {storedAccount.AccountNumber}.");
            Console.WriteLine($"Customer: {storedAccount.UserName} {storedAccount.UserSurname}.");
            Console.WriteLine($"Account currency: {storedAccount.AccountCurrency}.");
            Console.WriteLine($"Initial balance deposited upon account creation: {storedAccount.Balance}  {storedAccount.AccountCurrency}.");
        }
        public bool doesAccountExist(long accountNum)
        {
            return accounts.ContainsKey(accountNum) ? true : false;
        }
        public Account findAccount(long accountNum)
        {
            Account userAccount = accounts[accountNum];
            return userAccount;
        }
        //user login and logout mehods
        public bool loginUser(long accountNum)
        {
            if (!doesAccountExist(accountNum))
            {
                Console.WriteLine("The login attempt failed.  The user with this account number does not exist.");
                return false;
            }
            else
            {
                Account user = findAccount(accountNum);
                user.IsLoggedIn= true;
                //accounts[accountNum].IsLoggedIn = true;
                Console.WriteLine($"The login attempt successful. Welcome {user.UserName} {user.UserSurname}.");
                return true;
            }

        }
        public bool logoutUser(long accountNum)
        {

            if (!doesAccountExist(accountNum))
            {
                Console.WriteLine("cant log out user, the user with this accoun number doesnt exist"); 
                return false;

            }
            else
            {
                accounts[accountNum].IsLoggedIn = false;
                return true;

            }


        }
        public bool isUserLoggedIn(long accountNum)
        {

            if (accounts.ContainsKey(accountNum))
            {
                return accounts[accountNum].IsLoggedIn ? true : false;

            }
            else
            {
                return false;
            }

        }
        public void loggedUserActions(long accountNum, accountActions action)
        {
           // Console.WriteLine("accoun actions called with action: "+ action);
            Account userAccount= findAccount(accountNum);
           
            switch(action)
            {
                case accountActions.CheckBalance:
                    userAccount.rerieveBalance();
                    break;

                case accountActions.TransactionHistory:
                   userAccount.retrieveTTransactionHistory();
                    break;
                case accountActions.WithdrawATM:
                    userAccount.withdrawFromATM(accountNum); 
                    break;
                case accountActions.DepositATM:
                    userAccount.depositToATM(accountNum);
                    break;
                case accountActions.SendMoney:
                    long recipientAccountNumber;
                    Console.WriteLine("What is the recipient you would like to send to? Provide accoun number (long)");
                    long.TryParse(Console.ReadLine(), out recipientAccountNumber);
                    Account recepientAccount = findAccount(recipientAccountNumber);
                    userAccount.sendBetweenAccounts(accountNum, recepientAccount);


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
       
        private void depositOnAccount(long accountNum, bankingOperationTypes transactionType) {
            long recipientAccount;
            bankingOperationTypes userOperation;
            Console.WriteLine("Which accoun woul you like to send money to? (long)");
            long.TryParse(Console.ReadLine(), out recipientAccount);

            Console.WriteLine("What operat");

          //  findAccount(accountNum).performTransaction(recipientAccount);
        }   

    }
}
