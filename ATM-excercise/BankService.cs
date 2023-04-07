using ATM_excercise.Raven;
using Newtonsoft.Json.Linq;
using Raven.Client.Documents.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Raven.Client.Documents.Smuggler.SmugglerProgressBase;

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

        //private long accountsCreatedUntilNow = 0;
        #region account creation

        public long CreateAccount(string name, string surname, Currency currency)
        {
            return CreateAccount(name, surname, currency, 0);
        }

        public long CreateAccount(string name, string surname, Currency currency, decimal initialBalance)
        {
            try
            {
                long accountNum = GenerateNextAccountNumber();
                Account newAccount = new Account(accountNum, name, surname, currency, initialBalance);

                if (initialBalance > 0)
                {
                    BankDeposit initialDeposit = new BankDeposit(accountNum, initialBalance, currency);
                    newAccount.TransactionHistory.Add(initialDeposit);
                }

                using (var session = DocumentStoreHolder.Store.OpenSession())
                {
                    session.Store(newAccount);
                    session.SaveChanges();
                }
                return accountNum;
            }
            catch (Exception ex)
            { 
                throw new Exception("Couldn't create new account", ex);
            }
        }

        private long GenerateNextAccountNumber()
        {
            try
            {
                bool isUniqueAccountNumber = false;
                long randomlyGeneratedAccountNumber;
                do
                {
                    Random generator = new Random();
                    long.TryParse(generator.Next(0, 999999).ToString("D3"), out randomlyGeneratedAccountNumber); //somethign cn go wrong
                    Account existingAccount = GetAccount(randomlyGeneratedAccountNumber);
                    isUniqueAccountNumber = (existingAccount == null) ? true : false;
                } while (isUniqueAccountNumber == false);

                return randomlyGeneratedAccountNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region account actions

        public bool LogUserIntoAccount(long accountNum, out Account updatedAccount)
        {

            Account currentAccount = GetAccount(accountNum);


            if (currentAccount != null)
            {
                using (var session = DocumentStoreHolder.Store.OpenSession())
                {
                    updatedAccount = session.Query<Account>().Single(acc => acc.AccountNumber == accountNum);
                    updatedAccount.IsLoggedIn = true;
                    session.SaveChanges();
                    return true;
                }
            }
            else
            {
                updatedAccount = null;
                return false;
            }
        }

        public bool LogUserOutFromAccount(long accountNum, out Account updatedAccount)
        {
            Account currentAccount = GetAccount(accountNum);

            if (currentAccount != null)
            {
                using (var session = DocumentStoreHolder.Store.OpenSession())
                {
                    updatedAccount = session.Query<Account>().Single(acc => acc.AccountNumber == accountNum);
                    updatedAccount.IsLoggedIn = false;
                    session.SaveChanges();
                    return true;
                }
            }
            else
            {
                updatedAccount = null;
                return false;
            }
        }

        public Account GetAccount(long accountNum)
        {
            List<Account> accounts = new List<Account>();
            //unit of work pattern
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                accounts = session.Query<Account>().Where(account => account.AccountNumber == accountNum).ToList();
            }

            if (accounts.Count != 0)
            {
                return accounts[0];
            }
            return null;
        }

        public decimal UpdateBalance(long accountNum, decimal newTransactionAmount) //TOFIX- doesnt update balance after tranfer correctly
        {
            Account currentAccount = GetAccount(accountNum);

            if (currentAccount != null)
            {
                using (var session = DocumentStoreHolder.Store.OpenSession())
                {
                    Account updatedAccount = session.Query<Account>().Single(acc => acc.AccountNumber == accountNum);
                    updatedAccount.Balance += newTransactionAmount;
                    session.SaveChanges();
                    return updatedAccount.Balance;
                }
            }
            else
            {
                return currentAccount.Balance;
            }
        }

        public Transaction AddTransactionToTransactionHistory(long accountNum, Transaction transaction)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                Account updatedAccount = session.Query<Account>().Single(acc => acc.AccountNumber == accountNum);
                updatedAccount.TransactionHistory.Add(transaction);
                session.SaveChanges();
            }
            return transaction;

        }

        #endregion

        #region account transactions
        public ATMTransaction DepositToATM(long accountNum, decimal amount)
        {
            Account userAccount = GetAccount(accountNum);
            ATMTransaction transaction = new ATMTransaction(amount, accountNum, userAccount.AccountCurrency);
            UpdateBalance(accountNum, amount);
            AddTransactionToTransactionHistory(accountNum, transaction);
            //userAccount.UpdateBalance(amount);
            //userAccount.AddTransactionToTransactionHistory(transaction);
            return transaction;
        }

        public ATMTransaction WithdrawFromATM(long accountNum, decimal amount)
        {
            Account userAccount = GetAccount(accountNum);
            ATMTransaction transaction = new ATMTransaction(amount * (-1), accountNum, userAccount.AccountCurrency);
            UpdateBalance(accountNum, (-1) * amount);
            AddTransactionToTransactionHistory(accountNum, transaction);
            //userAccount.UpdateBalance((-1) * amount);
            //userAccount.AddTransactionToTransactionHistory(transaction);
            return transaction;
        }

        //public BankTransfer TransferToAccount(long senderAccountNumber, long recipientAccountNumber, decimal amount)

        //{
        //    Account senderAccount = GetAccount(senderAccountNumber);
        //    Account recipientAccount = GetAccount(recipientAccountNumber);





        //    BankTransfer transactionOutgoing = new BankTransfer(senderAccountNumber, recipientAccount.AccountNumber, BankTransferType.Outgoing, amount * (-1), senderAccount.AccountCurrency);
        //    UpdateBalance(senderAccountNumber, (-1) * amount);
        //    AddTransactionToTransactionHistory(senderAccountNumber, transactionOutgoing);

        //    senderAccount.UpdateBalance((-1) * amount);
        //    senderAccount.AddTransactionToTransactionHistory(transactionOutgoing);
        //    //OFIX: transacttion incoming doesnt behave i should - group them ogether in one session- anoher method.
        //    BankTransfer transacionIncoming = new BankTransfer(senderAccountNumber, recipientAccountNumber, BankTransferType.Incoming, amount, senderAccount.AccountCurrency);
        //    recipientAccount.UpdateBalance(amount);
        //    recipientAccount.AddTransactionToTransactionHistory(transacionIncoming);

        //    return transactionOutgoing;
        //}

        public BankTransfer TransferToAccount(Account senderAccount, Account recipientAccount, decimal amount)

        {
            BankTransfer transactionOutgoing = new BankTransfer(senderAccount.AccountNumber, recipientAccount.AccountNumber, BankTransferType.Outgoing, senderAccount.AccountCurrency, amount * (-1), senderAccount.AccountCurrency);         
            decimal incommingConvertedAmount = Math.Round(CurrencyConverter.ConvertBetweenCurrencies(amount, senderAccount.AccountCurrency, recipientAccount.AccountCurrency), 3);
            BankTransfer transactionIncoming = new BankTransfer(senderAccount.AccountNumber, recipientAccount.AccountNumber,BankTransferType.Incoming, senderAccount.AccountCurrency, incommingConvertedAmount, recipientAccount.AccountCurrency );

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                Account ravenSenderAccount = session.Query<Account>().Where(account => account.AccountNumber == senderAccount.AccountNumber).ToList()[0];
                Account ravenRecipientAccount = session.Query<Account>().Where(account => account.AccountNumber == recipientAccount.AccountNumber).ToList()[0];

                ravenSenderAccount.Balance -= amount;
                ravenRecipientAccount.Balance += incommingConvertedAmount;

                ravenSenderAccount.TransactionHistory.Add(transactionOutgoing);
                ravenRecipientAccount.TransactionHistory.Add(transactionIncoming);

                session.SaveChanges();
            }
            return transactionOutgoing;
        }
        #endregion

    }
}