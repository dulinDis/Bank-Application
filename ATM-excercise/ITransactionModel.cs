using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    public interface ITransactionModel
    {
        long TransactionId { get; set; }
        decimal Amount { get; set; }
        Currency Currency { get; set; }
        DateTime CreatedAt { get; set; }

        void PerformBankTransaction(Transaction transaction);
    }
}
