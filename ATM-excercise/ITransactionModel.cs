using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
     public interface ITransactionModel
    {
       //   decimal amount { get; set; }

        public void performBankTransaction(Transaction transaction)
        {
            Console.WriteLine("transaction performed");
        }
    }
}
