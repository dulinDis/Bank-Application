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
        private string userName;
        private string userSurname;
        private decimal balance;
        private long accountNumber;
        private bool isLoggedIn= false;
        private currencyOptions accountCurrency;
        private List<Transaction> transactionHistory; 

        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public decimal Balance { get; set; }
        public long AccountNumber { get; }
        public bool IsLoggedIn { get; set; }

        public currencyOptions AccountCurrency { get; set; }

       public List<Transaction> TransactionHistory { get; set; }
       // public List<ITransferable> TransactionHistory { get; set; }

        public Account (long accountNumber, string userName, string userSurname, currencyOptions currencyOption, decimal initialBalance=0)
        {
            UserName = userName;
            UserSurname = userSurname;
            Balance += initialBalance;
            AccountNumber = accountNumber;
            AccountCurrency = currencyOption;
             TransactionHistory = new List<Transaction>(); 
           // TransactionHistory = new List<ITransferable>();
        }

        private bool isTransactionValid(decimal amount)
        {
            if (amount <= 0 || Balance < 0 || amount>Balance)
            {
                return false;
            }         


            else return true;
        }


       /* private void Importar2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { }
        }*/
       public void sendBetweenAccounts(long accountNumber)
        {
            decimal amount;
            long recipientAccount;
            // bool escPressed = false;
            bool endLoop = false;

            while (!endLoop)
            {
                Console.WriteLine("how much would you like to send? (decimal)?");
                decimal.TryParse(Console.ReadLine(), out amount);
                Console.WriteLine("What is the recipient you would like to send to? Provide accoun number (long)");
                long.TryParse(Console.ReadLine(), out recipientAccount);

                if (Console.KeyAvailable && (Console.ReadKey(true).Key == ConsoleKey.Escape || Console.ReadKey().KeyChar == 'c'))
                {
                    Console.WriteLine("esc pressed"
                        ); break;
                }
                else if (!isTransactionValid(amount) || recipientAccount<=0)
                {
                    //conditional for account currency
                    Console.WriteLine("invalid operation.");
                   // Console.WriteLine($"Your  current balannce: {Balance} {AccountCurrency}. You tried to withdraw {amount} {AccountCurrency}");
                    Console.WriteLine($"Try again.");


                    // return;
                }
                else
                {
                    BankTransfer transactionOutgoing = new BankTransfer(accountNumber,recipientAccount,transferType.outgoing, amount,AccountCurrency);
                    Balance -= amount;

                    // BankDeposit transacionIncoming = new BankTransfer(accountNumber, recipientAccount, transferType.incoming, amount, AccountCurrency);
                    transactionOutgoing.displayBankTransferDetails();
                    // TransactionHistory.Add(transactionOutgoing);
                    //transaction.displayBankTransferDetails();
                    endLoop = true;
                }
            }





        }
        public void depositToATM(long accountNumer)
        {
            decimal amount;
            // bool escPressed = false;
            bool endLoop = false;

            while (!endLoop)
            {
                Console.WriteLine("how much would you like depositt? (decimal)? To escape press esc on your keyboard");
                decimal.TryParse(Console.ReadLine(), out amount);
                if (Console.KeyAvailable && (Console.ReadKey(true).Key == ConsoleKey.Escape || Console.ReadKey().KeyChar == 'c'))
                {
                    Console.WriteLine("esc pressed"
                        ); break;
                }
                else if (!isTransactionValid(amount))
                {
                    Console.WriteLine("invalid operation.");
                    Console.WriteLine($"Your tried to deposit negative number.");
                    Console.WriteLine($"Try again.");


                    // return;
                }
                else
                {
                    ATMTransaction transaction = new ATMTransaction(amount, accountNumber, AccountCurrency);
                    // transaction.performBankTransaction(transaction);
                    //transaction.performBankTransaction(transaction);
                    Balance += amount;
                    TransactionHistory.Add(transaction);
                    transaction.displayATMTransactionDetails();
                    endLoop = true;
                }
            }
        }
        public void withdrawFromATM(long accountNumber)
        {
            decimal amount;
           // bool escPressed = false;
            bool endLoop = false;

            while (!endLoop)
            {
                Console.WriteLine("how much would you like to withdraw? (decimal)? To escape press esc on your keyboard");
                decimal.TryParse(Console.ReadLine(), out amount);
                if (Console.KeyAvailable && (Console.ReadKey(true).Key == ConsoleKey.Escape || Console.ReadKey().KeyChar == 'c')) {
                    Console.WriteLine(  "esc pressed"
                        ); break; }
                else if (!isTransactionValid(amount))
                {
                    Console.WriteLine("invalid operation.");
                    Console.WriteLine($"Your  current balannce: {Balance} {AccountCurrency}. You tried to withdraw {amount} {AccountCurrency}");
                    Console.WriteLine($"Try again.");


                    // return;
                }
                else
                {
                    ATMTransaction transaction = new ATMTransaction(amount * (-1), accountNumber, AccountCurrency);
                    // transaction.performBankTransaction(transaction);
                    //transaction.performBankTransaction(transaction);
                    Balance -= amount;
                    TransactionHistory.Add(transaction);
                    transaction.displayATMTransactionDetails();
                    endLoop = true;
                }
            }

         
            /* previously working solution
             * 
             * 
             * 
             * 
             *  if (!isTransactionValid(amount))
                {
                    Console.WriteLine("invalid operation.");

                    Console.WriteLine($"Your  current balannce: {Balance} {AccountCurrency}. You tried to withdraw {amount} {AccountCurrency}");
                    Console.WriteLine($"Try again.");

                   
                   // return;
                }
                else
                {
                    ATMTransaction transaction = new ATMTransaction(amount * (-1), accountNumber, AccountCurrency);
                    //transaction.performBankTransaction(transaction);
                    Balance -= amount;
                    TransactionHistory.Add(transaction);
            }
             * */










        }//beter rerun bool so tha in bank you could repeat anoher operation 

        /*public void performTransaction(decimal amount, bankingOperationTypes transactionType, Transaction transaction)
        {
            balance += amount;
            TransactionHistory.Add(transaction);
        }*/

        public void rerieveBalance()
        {
            Console.WriteLine($"Current balance for the account {AccountNumber}: {Balance} {accountCurrency}");
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

        /// <summary>
        /// private bool sendMoney(Transaction )
        /// </summary>
        /// <param name="recipientsAccount"></param>
        public void performTransaction(Account recipientsAccount)
        {
            if (this.AccountNumber == recipientsAccount.AccountNumber)
            {
                Console.WriteLine("the beneficiary's account number is same like remitter"); 
            } else
            {


            }
        }

        // public bool isUserLoggedIn

        /*     public void PerformATMTransaction(double amount, ATMTransaction transaction)
             {
                 balance += amount;
                 TransactionHistory.Add(transaction);
             }

             public void PerformBankTransfer(double amount, BankTransfer transaction)
             {
                 balance += amount;
                 TransactionHistory.Add(transaction);
             }

             public void PerformBankDeposit(double amount, BankDeposit transaction)
             {
                 balance += amount;
                 TransactionHistory.Add(transaction);
             }*/


        /*  public void Withdraw (double amount, Transaction transaction)
          {
              balance -= amount;
              TransactionHistory.Add(transaction);
          }
          public void Deposit (double amount, Transaction transaction)
          {
              balance += amount;
              TransactionHistory.Add(transaction);
          }


          public void Transfer(double amount, Transaction transaction)
          {
              balance -= amount;
              TransactionHistory.Add(transaction);
          }*/

    }
}
