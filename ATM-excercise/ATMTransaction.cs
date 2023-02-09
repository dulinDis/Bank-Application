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

        private optionTypes _type;
        private long _accountNumber;

        public long AccountNumber { get
            {
                return _accountNumber;
            }
            set 
            { 
                if (value > 0) 
                {
                    _accountNumber = value;
                        }
            }
        }

        public optionTypes Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        public ATMTransaction(decimal amount, long accountNumber, CurrencyOptions currencyOption) : base (amount, currencyOption)
        {
            Type = GetTransactionType(amount);
            AccountNumber = accountNumber;
        }

        private optionTypes GetTransactionType(decimal amount)
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
        public void DisplayATMTransactionDetails()
        {
            Console.WriteLine($"New transaction with transaction ID {TransactionID} created on {CreatedAt} for value {Amount} {Currency}. Transaction type: {Type}");
        }



       //  public void performBankTransaction(ATMTransaction transaction)
      //    {
      //      Console.WriteLine("atm transaction performed");
        //  }

    }
}
