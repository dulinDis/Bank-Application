/* using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    internal class Transaction
    {

      //  Hashtable typeOptions = new Hashtable();
     //   typeOptions.Add("0", "withdrawal");
     //       typeOptions.Add("1", "deposit");


        private double transactionID;
        private double amount;
      //  private string type;
        private long recepientAccount;

        public double TransactionID
        {
            get
            {
                return amount;
            }
            set
            {
                transactionID = value; //some random nummber assigns
            }


        }
        public double Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (value > 0)
                {
                    amount = value;
                }
            }
        };
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (amount > 0)
                {
                    type = value;
                }
            }
        }

        public long RecepientAccount
        {
            get { return recepientAccount; }
            set
            {
                if (recepientAccount > 0)
                {
                    recepientAccount = value;
                }
            }
        }
        public Transaction(double amount, long recepientAccount) {
            Amount = amount;
            RecepientAccount = recepientAccount;
            transactionID = generateTransactionID();
        }

        private double generateTransactionID()
        {
            throw new NotImplementedException();
        }
    }  

   
    
}
*/