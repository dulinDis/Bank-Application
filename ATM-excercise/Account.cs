using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
namespace ATM_excercise
{
    internal class Account : ITransactionModel
    {
        private string _userName;
        private string _userSurname;
        private decimal _balance;
        private long _accountNumber;
        private bool _isLoggedIn= false;
        private CurrencyOptions _accountCurrency;
        private List<Transaction> _transactionHistory; 

        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public decimal Balance { get; set; }
        public long AccountNumber { get; }
        public bool IsLoggedIn { get; set; }

        public CurrencyOptions AccountCurrency { get; set; }

       public List<Transaction> TransactionHistory { get; set; }
       // public List<ITransferable> TransactionHistory { get; set; }

        public Account (long accountNumber, string userName, string userSurname, CurrencyOptions currencyOption, decimal initialBalance=0)
        {
            UserName = userName;
            UserSurname = userSurname;
            Balance += initialBalance;
            AccountNumber = accountNumber;
            AccountCurrency = currencyOption;
             TransactionHistory = new List<Transaction>(); 
           // TransactionHistory = new List<ITransferable>();
        }

        private bool CheckIfTransactionIsValid(decimal amount)
        {
            if (amount <= 0 || Balance < 0)
            {
                return false;
            }         


            else return true;
        }


       /* private void Importar2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { }
        }*/
       public void SendBetweenAccounts(long accountNumber, Account recipientAccount)
        {
            decimal amount;
            //long recipientAccount;
            // bool escPressed = false;
            bool endLoop = false;

            while (!endLoop)
            {
                Console.WriteLine("how much would you like to send? (decimal)?");
                decimal.TryParse(Console.ReadLine(), out amount);
                //Console.WriteLine("What is the recipient you would like to send to? Provide accoun number (long)");
               // long.TryParse(Console.ReadLine(), out recipientAccount);

                if (Console.KeyAvailable && (Console.ReadKey(true).Key == ConsoleKey.Escape || Console.ReadKey().KeyChar == 'c'))
                {
                    Console.WriteLine("esc pressed"
                        ); break;
                }
                else if (!CheckIfTransactionIsValid(amount) || recipientAccount .AccountNumber<= 0)
                {
                    //conditional for account currency
                    Console.WriteLine("invalid operation.");
                   // Console.WriteLine($"Your  current balannce: {Balance} {AccountCurrency}. You tried to withdraw {amount} {AccountCurrency}");
                    Console.WriteLine($"Try again.");


                    // return;
                }
                else
                {
                    BankTransfer transactionOutgoing = new BankTransfer(accountNumber, recipientAccount.AccountNumber, transferType.outgoing, amount*(-1),AccountCurrency);
                    Balance -= amount;      
                    TransactionHistory.Add(transactionOutgoing);
                    transactionOutgoing.DisplayBankTransferDetails();

                    BankTransfer transacionIncoming = new BankTransfer(accountNumber, recipientAccount.AccountNumber, transferType.outgoing, amount, AccountCurrency);
                    //BankDeposit transacionIncoming = new BankTransfer(recipientAccount.AccountNumber, transferType.incoming, amount, AccountCurrency);
                    recipientAccount.Balance += amount;
                    recipientAccount.TransactionHistory.Add(transacionIncoming);
                   // transacionIncoming.displayBankTransferDetails();
                   // Console.WriteLine( $"incomign balance for account no: {recipientAccount.AccountNumber} is for value {transacionIncoming.Amount}");
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
                Console.WriteLine("how much would you like depositt? (decimal)? To escape press esc on your keyboard");
                decimal.TryParse(Console.ReadLine(), out amount);
               
                if (!CheckIfTransactionIsValid(amount))
                {
                    Console.WriteLine("invalid operation.");
                    Console.WriteLine($"Your tried to deposit negative number.");
                    Console.WriteLine($"Try again.");


                    // return;
                }
                else
                {
                    ATMTransaction transaction = new ATMTransaction(amount, _accountNumber, AccountCurrency);
                    // transaction.performBankTransaction(transaction);
                    //transaction.performBankTransaction(transaction);
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
                 if (!CheckIfTransactionIsValid(amount) || amount > Balance)
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
        }//beter rerun bool so tha in bank you could repeat anoher operation 

        /*public void performTransaction(decimal amount, bankingOperationTypes transactionType, Transaction transaction)
        {
            balance += amount;
            TransactionHistory.Add(transaction);
        }*/

        public void RerieveBalance()
        {
            Console.WriteLine($"Current balance for the account {AccountNumber}: {Balance} {_accountCurrency}");
        }

        public void retrieveTTransactionHistory()
        {
            Console.WriteLine("Transaction history:");
           if (TransactionHistory.Count>0)
            {
                foreach (var transaction in TransactionHistory)
                {
                    Console.WriteLine($"Transaction made on {transaction.CreatedAt}. Value: {transaction.Amount} {transaction.Currency}");
                    /*  if (transaction is ATMTransaction)
                       {
                           Console.WriteLine();
                       }*/
                }
            } else
            {
                Console.WriteLine("No transactions registered on this account.");
            }
        }

         public void PerformTransaction(Account recipientsAccount)
        {
            if (this.AccountNumber == recipientsAccount.AccountNumber)
            {
                Console.WriteLine("the beneficiary's account number is same like remitter"); 
            } else
            {


            }
        }

    }
}
