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

        public void TransferToAccount(Account recipientAccount)
        {
            decimal amount;
            bool endLoop = false;

            while (!endLoop)
            {
                Console.WriteLine("How much would you like to send (decimal)?");
                decimal.TryParse(Console.ReadLine(), out amount);

                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("esc pressed");
                    break;
                }
                else if (amount <= 0 && Balance < amount)
                {
                    Console.WriteLine("Invalid operation.");
                    Console.WriteLine($"Try again.");
                }
                else
                {
                    BankTransfer transactionOutgoing = new BankTransfer(AccountNumber, recipientAccount.AccountNumber, BankTransferType.Outgoing, amount * (-1), AccountCurrency);
                    Balance -= amount;
                    TransactionHistory.Add(transactionOutgoing);
                    transactionOutgoing.DisplayBankTransferDetails();
                    BankTransfer transacionIncoming = new BankTransfer(AccountNumber, recipientAccount.AccountNumber, BankTransferType.Outgoing, amount, AccountCurrency);
                    recipientAccount.Balance += amount;
                    recipientAccount.TransactionHistory.Add(transacionIncoming);
                    endLoop = true;
                }
            }
        }


        public void DepositToATM(long accountNumer)
        {
            decimal amount;
            bool endLoop = false;

            while (!endLoop)
            {
                Console.WriteLine("How much would you like deposit (decimal)? To escape press 'esc' on your keyboard."); // not working
                decimal.TryParse(Console.ReadLine(), out amount);

                if (amount <= 0)
                {
                    Console.WriteLine("invalid operation.");
                    Console.WriteLine($"Your tried to deposit negative number.");
                    Console.WriteLine($"Try again.");
                }
                else
                {
                    ATMTransaction transaction = new ATMTransaction(amount, AccountNumber, AccountCurrency);
                    Balance += amount;
                    TransactionHistory.Add(transaction);
                    transaction.DisplayATMTransactionDetails();
                    endLoop = true;
                }
            }
        }

        public void WithdrawFromATM(long accountNumber)
        {
            decimal amount;
            bool endLoop = false;

            while (!endLoop)
            {
                Console.WriteLine("how much would you like to withdraw? (decimal)? To escape press esc on your keyboard");
                decimal.TryParse(Console.ReadLine(), out amount);
                if (amount <= 0 || amount >= Balance)
                {
                    Console.WriteLine("invalid operation.");
                    Console.WriteLine($"Your  current balannce: {Balance} {AccountCurrency}. You tried to withdraw {amount} {AccountCurrency}");
                    Console.WriteLine($"Try again.");
                }
                else
                {
                    ATMTransaction transaction = new ATMTransaction(amount * (-1), accountNumber, AccountCurrency);
                    Balance -= amount;
                    TransactionHistory.Add(transaction);
                    transaction.DisplayATMTransactionDetails();
                    endLoop = true;
                }
            }
        }

        public void RerieveBalance()
        {
            Console.WriteLine($"Current balance for the account {AccountNumber}: {Balance} {AccountCurrency}");
        }

        public void RetrieveTransactionHistory()
        {
            Console.WriteLine("Transaction history:");
            if (TransactionHistory.Count > 0)
            {
                foreach (var transaction in TransactionHistory)
                {
                    Console.WriteLine($"Transaction made on {transaction.CreatedAt}. Value: {transaction.Amount} {transaction.Currency}. Type: {transaction.BankingOperationTypeDisplayText}");
                }
            }
            else
            {
                Console.WriteLine("No transactions registered on this account.");
            }
        }

        public void PerformTransaction(Account recipientsAccount)
        {
            if (this.AccountNumber == recipientsAccount.AccountNumber)
            {
                Console.WriteLine("The beneficiary's account number is same like remitter.");
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
