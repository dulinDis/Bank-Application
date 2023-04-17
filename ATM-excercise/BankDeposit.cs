using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    public class BankDeposit : Transaction
    {
        /// <summary>
        /// The account which received the deposit.
        /// </summary>
        public long RecipientAccount { get; set; }

        public override BankingOperationType BankingOperationType => BankingOperationType.BankDeposit;
        /// <summary>
        /// Creates new bank deposit.
        /// </summary>
        /// <param name="recipientAccount">The account which received the deposit.</param>
        /// <param name="amount">Amoun deposited.</param>
        /// <param name="currencyOption">Currency of deposited account.</param>
        public BankDeposit(long recipientAccount, decimal amount, Currency currencyOption) : base(amount, currencyOption)
        {
            RecipientAccount = recipientAccount;
        }   
    }
}
