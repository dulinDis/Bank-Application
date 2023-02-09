using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    internal class BankDeposit : Transaction //,ITransactionModel
    {
        private long _recipientAccount;
        public string type = "Bank Deposit";

        public long RecipientAccount
        {
            get { return _recipientAccount; }
            set
            {
                _recipientAccount = value;
            }
        }

        public BankDeposit(long recipientAccount, decimal amount, CurrencyOptions currencyOption) : base(amount, currencyOption)
        {
           RecipientAccount = recipientAccount;
        }

        public void DisplayBankTransferDetails()
        {
            Console.WriteLine($"New bank deposit with transaction ID {TransactionID} created on {CreatedAt} for value {Amount} {Currency}. Transaction type: {type}. receiving party account: {RecipientAccount}");

        }

        //public void performBankTransaction(BankDeposit transaction)
        //{
        //    Console.WriteLine("bank deposit performed");
       // }

    }
}
