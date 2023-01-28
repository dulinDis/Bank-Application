using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ATM_excercise
{
    public abstract  class Transaction //: ITransferable
    {
       // public enum currencyOptions { USD, EUR, PLN };

        private long transactionID;
        private decimal amount;
        public DateTime createdAt;
        private currencyOptions currency;
        public long TransactionID
        {
            get
            {
                return transactionID;
            }
            set
            {
                transactionID = value; //some random nummber assigns
            }


        }
        public decimal Amount
        {
            get
            {
                return amount;
            }
            set
            {
               
                    amount = value;
               
            }
        }
        public currencyOptions Currency { get
            {
                return currency;
            } set
            {
                currency = value;
            } 
        }

        public DateTime CreatedAt { get { return createdAt; } set { createdAt = value; } }
        public Transaction(decimal amount, currencyOptions currencyOption)
        {
            TransactionID = generateTransactionID();
            Amount = amount;
            CreatedAt = DateTime.Now;
            Currency = currency;
        }

        private long generateTransactionID()
        {
            return 44;
        }

      //  public void performBankTransaction(Transaction transaction)
      //  {
     //       Console.WriteLine("transaction performed");
      //  }

    }

}
