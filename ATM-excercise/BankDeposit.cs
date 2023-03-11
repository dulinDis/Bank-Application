using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    public class BankDeposit : Transaction
    {
        public long RecipientAccount { get; set; }

        public override BankingOperationType BankingOperationType => BankingOperationType.BankDeposit;

        public BankDeposit(long recipientAccount, decimal amount, Currency currencyOption) : base(amount, currencyOption)
        {
            RecipientAccount = recipientAccount;
        }   
    }
}
