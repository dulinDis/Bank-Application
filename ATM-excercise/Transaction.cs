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

        private long _transactionID;
        private decimal _amount;
        public DateTime _createdAt;
        private CurrencyOptions _currency;
        public long TransactionID
        {
            get
            {
                return _transactionID;
            }
            set
            {
                _transactionID = value; //some random nummber assigns
            }


        }
        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
               
                    _amount = value;
               
            }
        }
        public CurrencyOptions Currency { get
            {
                return _currency;
            } set
            {
                _currency = value;
            } 
        }

        public DateTime CreatedAt { get { return _createdAt; } set { _createdAt = value; } }
        public Transaction(decimal amount, CurrencyOptions currencyOption)
        {
            TransactionID = generateTransactionID();
            Amount = amount;
            CreatedAt = DateTime.Now;
            Currency = _currency;
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
