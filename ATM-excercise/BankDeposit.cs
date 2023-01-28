using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    internal class BankDeposit : Transaction //,ITransactionModel
    {
        private long recipientAccount;
        public string type = "Bank Deposit";

        public long RecipientAccount
        {
            get { return recipientAccount; }
            set
            {

                recipientAccount = value;

            }
        }

        public BankDeposit(long recipientAccount, decimal amount, currencyOptions currencyOption) : base(amount, currencyOption)
        {
           RecipientAccount = recipientAccount;
        }

        public void displayBankTransferDetails()
        {
            Console.WriteLine($"New bank deposit with transaction ID {TransactionID} created on {CreatedAt} for value {Amount} {Currency}. Transaction type: {type}. receiving party account: {RecipientAccount}");

        }

        //public void performBankTransaction(BankDeposit transaction)
        //{
        //    Console.WriteLine("bank deposit performed");
       // }

    }
}
