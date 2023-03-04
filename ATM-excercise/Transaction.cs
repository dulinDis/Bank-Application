using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ATM_excercise
{
    public abstract class Transaction
    {
        public static long LastTransactionId = 0; // is it updated correctly? - check and fix if needed; alternatively GUID could be used

        public Transaction(decimal amount, Currency currency)
        {
            TransactionId = ++LastTransactionId;
            Amount = amount;
            CreatedAt = DateTime.Now;
            Currency = currency;
        }

        public long TransactionId { get; private set; }

        public decimal Amount { get; private set; }

        public Currency Currency { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public abstract BankingOperationType BankingOperationType { get; }

        public string BankingOperationTypeDisplayText => BankingOperationType switch
        {
            BankingOperationType.BankTransfer => "Bank transfer",
            BankingOperationType.ATMTransaction => "ATM transaction",
            BankingOperationType.BankDeposit => "Bank despoit",
            _ => throw new ArgumentOutOfRangeException(nameof(BankingOperationType))
        };

        //Alternative approach
        //public string GetTransactionTypeDisplayText => this switch
        //{
        //    BankTransfer => "Bank transfer",
        //    ATMTransaction => "ATM transaction",
        //    BankDeposit => "Bank despoit",
        //    _ => throw new ArgumentOutOfRangeException(this.GetType().Name)
        //};
    }
}
