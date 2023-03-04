using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ATM_excercise.Transaction;

namespace ATM_excercise
{
    public enum ATMTransactionType 
    { 
        Withdrawal,
        Deposit 
    }

    internal class ATMTransaction : Transaction
    {
        private long _accountNumber;

        public long AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (value <= 0)
                    throw new ArgumentException(nameof(AccountNumber));

                _accountNumber = value;
            }
        }

        public ATMTransactionType ATMTransactionType { get; set; }

        public override BankingOperationType BankingOperationType => BankingOperationType.ATMTransaction;

        public ATMTransaction(decimal amount, long accountNumber, Currency currencyOption) : base(amount, currencyOption)
        {
            ATMTransactionType = GetTransactionType(amount);
            AccountNumber = accountNumber;
        }

        private ATMTransactionType GetTransactionType(decimal amount)
        {
            return amount > 0 
                ? ATMTransactionType.Deposit 
                : ATMTransactionType.Withdrawal;
        }

        public void DisplayATMTransactionDetails()
        {
            Console.WriteLine($"New transaction with transaction ID {TransactionId} created on {CreatedAt} for value {Amount} {Currency}. Transaction type: {ATMTransactionType}");
        }
    }
}
