using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ATM_excercise.Transaction;

namespace ATM_excercise
{ 
    internal class ATMTransaction : Transaction //,ITransactionModel
    {

        public enum optionTypes { Wihdrawal, Deposit };
        private optionTypes type;
        private long accountNumber;

        public long AccountNumber { get
            {
                return accountNumber;
            }
            set 
            { 
                if (value > 0) 
                {
                    accountNumber = value;
                        }
            }
        }

        public optionTypes Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        public ATMTransaction(decimal amount, long accountNumber, currencyOptions currencyOption) : base (amount, currencyOption)
        {
            Type = getTransactionType(amount);
            AccountNumber = accountNumber;
        }

        private optionTypes getTransactionType(decimal amount)
        {
            if (amount > 0)
            {
                return optionTypes.Deposit;
            }
            else
            {
                return optionTypes.Wihdrawal;
            }
        }
        public void displayATMTransactionDetails()
        {
            Console.WriteLine($"New transaction with transaction ID {TransactionID} created on {CreatedAt} for value {Amount} {Currency}. Transaction type: {Type}");
        }



       //  public void performBankTransaction(ATMTransaction transaction)
      //    {
      //      Console.WriteLine("atm transaction performed");
        //  }

    }
}
