using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ATM_excercise.Transaction;

namespace ATM_excercise
{
    /// <summary>
    /// Possible ATM transacion types.
    /// </summary>
    public enum ATMTransactionType 
    { 
        Withdrawal,
        Deposit 
    }
    public class ATMTransaction : Transaction
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
        /// <summary>
        /// Creates new ATM Transaction.
        /// </summary>
        /// <param name="amount">Amount of transaction.</param>
        /// <param name="accountNumber">Account the transaction is performed on.</param>
        /// <param name="currencyOption">Transaction currency.</param>
        public ATMTransaction(decimal amount, long accountNumber, Currency currencyOption) : base(amount, currencyOption)
        {
            ATMTransactionType = GetTransactionType(amount);
            AccountNumber = accountNumber;
        }
        /// <summary>
        /// PRovides transacion type
        /// </summary>
        /// <param name="amount">Amount of transaction.</param>
        /// <returns>enum ATMTransactionype</returns>
        private ATMTransactionType GetTransactionType(decimal amount)
        {
            return amount > 0 
                ? ATMTransactionType.Deposit 
                : ATMTransactionType.Withdrawal;
        }
    }
}
