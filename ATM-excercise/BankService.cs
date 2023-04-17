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
using static ATM_excercise.Utils;

namespace ATM_excercise
{
    // TODO:  multicurrrency account option
    public enum Currency
    {
        USD,
        EUR,
        PLN
    }
    //public enum AccountActions
    //{
    //    CheckBalance,
    //    TransactionHistory,
    //    WithdrawATM,
    //    DepositATM,
    //    SendMoney
    //}

    /// <summary>
    /// Possible banking operation types.
    /// </summary>
    public enum BankingOperationType
    {
        ATMTransaction,
        BankTransfer,
        BankDeposit
    }
    public class BankService
    {
        #region Account Creation

        /// <summary>
        /// Creates new user account in the database.
        /// </summary>
        /// <param name="name">First name.</param>
        /// <param name="surname">Last name.</param>
        /// <param name="currency">Account Currency.</param>
        /// <returns></returns>
        public long CreateAccount(string name, string surname, Currency currency)
        {
            return CreateAccount(name, surname, currency, 0);
        }
        /// <summary>
        /// Creates new user account in the database.
        /// </summary>
        /// <param name="name">First name.</param>
        /// <param name="surname">Last name.</param>
        /// <param name="currency">Account Currency.</param>
        /// <param name="initialBalance">Initial balance.</param>
        /// <returns>Newly created account number.</returns>
        public long CreateAccount(string name, string surname, Currency currency, decimal initialBalance)
        {
            //if (surname == "Złodziej")
            //    throw new CannotCreateAccountException();

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

        //public class CannotCreateAccountException : Exception
        //{
        //    public CannotCreateAccountReason Reason { get; set; }
        //}

        public enum CannotCreateAccountReason
        {
            CustomerIsNotCitizen,
            CustomerIsAConvict
        }
        /// <summary>
        /// Generates account number for a new account.
        /// </summary>
        /// <returns>Unique account number that doesn't appear in the database.</returns>
        private long GenerateNextAccountNumber()
        {
            try
            {
                bool isUniqueAccountNumber = false;
                long randomlyGeneratedAccountNumber;
                do
                {
                    Random generator = new Random();
                    long.TryParse(generator.Next(0, 999999).ToString("D3"), out randomlyGeneratedAccountNumber);
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

        #region Account Actions
        /// <summary>
        /// Logs the user into the account.
        /// </summary>
        /// <param name="accountNum">Account to perform login on</param>
        /// <param name="updatedAccount">Account object.</param>
        /// <returns>Bool if action successful.</returns>
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
        /// <summary>
        /// Logs the user out from the account.
        /// </summary>
        /// <param name="accountNum">Account to perform logout on</param>
        /// <param name="updatedAccount">Account object.</param>
        /// <returns>Bool if action successful.</returns>
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

        /// <summary>
        /// Returns from database searched account.
        /// </summary>
        /// <param name="accountNum">Searched account number.</param>
        /// <returns>Returns account object.</returns>
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
        /// <summary>
        /// Updates the account balance
        /// </summary>
        /// <param name="accountNum">Account which balance is updated.</param>
        /// <param name="newTransactionAmount">The value of transaction that affected balance.</param>
        /// <returns>Returns updated balance.</returns>
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
        /// <summary>
        /// Adds transaction performed on the account to its transaction history.
        /// </summary>
        /// <param name="accountNum">Account on which the transaction took place.</param>
        /// <param name="transaction">Performed transaction.</param>
        /// <returns>Returns transaction object.</returns>
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

        #region Account Transactions.
        /// <summary>
        /// Deposits funds on the account.
        /// </summary>
        /// <param name="accountNum">Account that gets funds.</param>
        /// <param name="amount">Amount to be deposited.</param>
        /// <returns>Returns transaction object.</returns>
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
        /// <summary>
        /// Withdraws funds from the account.
        /// </summary>
        /// <param name="accountNum">Debited account number.</param>
        /// <param name="amount">Amount to be withdrawn.</param>
        /// <returns>Returns transaction object.</returns>
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
        /// <summary>
        /// Transfers funds from one account to another one.
        /// </summary>
        /// <param name="senderAccount">Sender Account</param>
        /// <param name="recipientAccount">Recipient Account</param>
        /// <param name="amount">Amount to be transferred between accounts.</param>
        /// <returns>Returns outgoing transaction object.</returns>
        public BankTransfer TransferToAccount(Account senderAccount, Account recipientAccount, decimal amount)
        {
            BankTransfer transactionOutgoing = new BankTransfer(senderAccount.AccountNumber, recipientAccount.AccountNumber, BankTransferType.Outgoing, senderAccount.AccountCurrency, amount * (-1), senderAccount.AccountCurrency);
            decimal incommingConvertedAmount = Math.Round(CurrencyConverter.ConvertBetweenCurrencies(amount, senderAccount.AccountCurrency, recipientAccount.AccountCurrency), 3);
            BankTransfer transactionIncoming = new BankTransfer(senderAccount.AccountNumber, recipientAccount.AccountNumber, BankTransferType.Incoming, senderAccount.AccountCurrency, incommingConvertedAmount, recipientAccount.AccountCurrency);

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